using AnimeAB.Reponsitories;
using AnimeAB.Reponsitories.Interface;
using AnimeAB.Core.Validator.Filter;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using AnimeAB.Reponsitories.Reponsitories.Account;
using Microsoft.AspNetCore.Routing;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using AnimeAB.Reponsitories.Utils;
using AnimeAB.Core.ChatHubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AnimeAB.Core
{
    public class Startup
    {
        public static IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string AnimeABClientCors = "animeab";
        readonly string LocalClient = "https://animeab.tk";
        readonly string LocalhostDev = "http://localhost:3000";

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
            var configFirebase = Configuration.GetSection("Firebase");
            AppSettingFirebase appSettingFirebase = configFirebase.Get<AppSettingFirebase>();
            //Enable cors
            services.AddCors(options =>
            {
                options.AddPolicy(AnimeABClientCors,
                        builder =>
                        {
                            builder.WithOrigins(LocalClient, LocalhostDev)
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
            //Authenticate cookies default and jwt
            services.AddAuthentication(opt =>
            {
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }) //Cookies Authentication
               .AddCookie(options =>
               {
                   //Dia chi tra ve khi khon duoc phep truy cap
                   options.AccessDeniedPath = new PathString("/Unauthorized");

                   options.Cookie = new CookieBuilder
                   {
                       //Domain = "",
                       HttpOnly = true,
                       Name = ".animeab.Security.Cookie",
                       Path = "/",
                       SameSite = SameSiteMode.Strict,
                       SecurePolicy = CookieSecurePolicy.SameAsRequest
                   };
                   options.LoginPath = new PathString("/anime/login");
                   options.ReturnUrlParameter = "returnUrl";
               })
               .AddJwtBearer(options =>
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

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                //filter validation
                options.Filters.Add<ValidationFilter>();
            })
                //add fluent validator
                .AddFluentValidation(mvcConfiguration =>
                            mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                //enable return xml format
                .AddXmlDataContractSerializerFormatters()
                .AddRazorRuntimeCompilation();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureApplicationCookie(options =>
            {
                //Cookies setting
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
                //Dia chi tra ve khi chua dang nhap
                options.LoginPath = "/anime/Login";
                //Dia chi tra ve khong cho phep truy cap
                options.AccessDeniedPath = "/Unauthorized";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(opt =>
            {
                //Role admin
                opt.AddPolicy(RoleSchemes.Admin,
                    builder => builder.RequireRole(RoleSchemes.Admin));
            });
            //Route viet thuong
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAppContext>(x => new AppContext(x.GetService<IHttpContextAccessor>()));

            services.AddScoped<IUnitOfWork>(x => new UnitOfWork(appSettingFirebase));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(new ErrorDetails { 
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error"
                            }.ToString());
                        }
                    });
                });
            }

            app.UseCors(AnimeABClientCors);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<CommentHub>("/commentHub");
            });

        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
