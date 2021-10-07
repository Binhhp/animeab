using AnimeAB.ApiIntegration.ChatHubs;
using AnimeAB.Application.Common.ExceptionsHanlder.Middleware;
using AnimeAB.Domain.Settings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AnimeAB.ApiIntegration
{
    public class Startup
    {
        public static IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string AnimeABClientCors = "animeab";
        readonly string LocalClient = "https://animeab.co";
        public Startup(IWebHostEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsetting.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //settings firebase
            var configFirebase = Configuration.GetSection("Firebase");
            AppSettingFirebase appSettingFirebase = configFirebase.Get<AppSettingFirebase>();

            services.AddCors(options =>
            {
                options.AddPolicy(AnimeABClientCors,
                        builder =>
                        {
                            builder.WithOrigins(LocalClient)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .SetIsOriginAllowed(origin => true) // allow any origin
                                    .AllowCredentials();
                        });
            });

            //Memory caching
            services.AddResponseCaching();
            services.AddMemoryCache();
            //SignalIR use json
            services.AddSignalR();
            //Jwt Token
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = appSettingFirebase.JwtAuthFirebase;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = appSettingFirebase.JwtAuthFirebase,
                    ValidateAudience = true,
                    ValidAudience = appSettingFirebase.ProjectName,
                    ValidateLifetime = true
                };
                options.SaveToken = true;
            });

            services.AddControllers()
                //add fluent validator
                .AddFluentValidation(mvcConfiguration =>
                            mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                //enable return xml format
                .AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Route viet thuong
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                //cerberti https
                app.UseHsts();
            }
            //Enable cors
            app.UseCors(AnimeABClientCors);
            //Enable http => https
            app.UseHttpsRedirection();
            //response caching
            app.UseResponseCaching();
            //response compression
            app.UseResponseCompression();
            //middleware routing
            app.UseRouting();
            //middleware authentication: cookies auth, token auth
            app.UseAuthentication();
            //middleware authorization: policy base, role base, claim
            app.UseAuthorization();
            //setting router controller and api
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CommentHub>("/commentHub");
            });
        }
    }
}
