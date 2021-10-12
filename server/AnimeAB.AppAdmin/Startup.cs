
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AnimeAB.Domain.Settings;
using AnimeAB.Domain.ValueObjects;
using AnimeAB.Application.Reponsitories.Base;
using AnimeAB.Infrastructure.Persistence.Reponsitories.Base;
using AnimeAB.Application.Common.ExceptionsHanlder.Middleware;
using AnimeAB.Core;
using AnimeAB.AppAdmin.Validator.Filter;

namespace AnimeAB.AppAdmin
{
    public class Startup
    {
        public static IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

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

            //Memory caching
            services.AddResponseCaching();
            services.AddMemoryCache();
            //Authenticate cookies default and jwt
            services.AddAuthentication() //Cookies Authentication
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

            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/");
            }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                //cerberti https
            }
            //Enable http => https
            //app.UseHttpsRedirection();
            //response caching
            //app.UseResponseCaching();
            //response compression
            //app.UseResponseCompression();
            //middleware routing
            app.UseRouting();
            //middleware authentication: cookies auth, token auth
            app.UseAuthentication();
            //middleware authorization: policy base, role base, claim
            app.UseAuthorization();
            //setting router controller and api
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
