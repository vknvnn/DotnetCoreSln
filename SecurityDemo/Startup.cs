using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityWebApp.TokenHelper;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using SecurityDemo.Services.Resources;
using SecurityDemo.Services.Permissions;
using SecurityDemo.Services.Policies;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SecurityWebApp;
using SecurityDemo.Services.TestDI;
using SecurityWebApp.Filters;

namespace SecurityDemo
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = JwtHelper.GetTokenValidation("nghiepvo.com", "nghiepvo.com", "nghiepvo-secret-key");
                        options.Events = JwtHelper.GetTokenEvent();
                    });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("age-adult-policy", x => { x.AddRequirements(new MinAgeRequirement(18)); });
                options.AddPolicy("age-elder-policy", x => { x.AddRequirements(new MinAgeRequirement(42)); });
                options.AddPolicy("resource-allow-policy", x => { x.AddRequirements(new ResourceBasedRequirement()); });
            });

            services.AddSingleton<IAuthorizationHandler, MinAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, ResourceHandlerV1>();
            services.AddSingleton<IAuthorizationHandler, ResourceHandlerV2>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationHandler, AuthorizationHandlerCus>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITest, Test>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();            
            app.UseMvc(configs =>
            {
                configs.MapRoute(name: "default", template: "{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" });
            });
            ServiceProvider = app.ApplicationServices;            
        }
    }
}
