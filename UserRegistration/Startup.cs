using CookieAuthDemoProject.CustomHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserRegistration.Models;
using UserRegistration.Repository.Contracts;
using UserRegistration.Repository.Implementation;
using UserRegistration.Services.Contracts;
using UserRegistration.Services.Implementation;

namespace UserRegistration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication",
              config =>
              {
                  config.Cookie.Name = "UserLogin";
                  config.LoginPath = "/Login/UserLogin";
                  config.AccessDeniedPath = "/Login/UserAccessDenied";
              });
            services.AddAuthorization(config =>
            {
                var userAuthPolicyBuilder = new AuthorizationPolicyBuilder();
                config.DefaultPolicy = userAuthPolicyBuilder
                                    .RequireAuthenticatedUser()
                                    .Build();
            });
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
