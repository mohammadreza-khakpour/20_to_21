using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;

namespace MohammadpourAspNetCoreSaturdayMondayEvening
{

    //class Reza
    //{
    //}

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
            services.AddControllersWithViews();
            services.AddAuthentication();
            //services.AddTransient<ITest, Test1>();
            services.Configure<CookiePolicyOptions>(x=> {
                x.CheckConsentNeeded = y => false;
                x.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory
            , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            loggerFactory.AddFile($@"D:\MohammadpourLog_info.txt"
                , minimumLevel: LogLevel.Information);
            loggerFactory.AddFile($@"D:\MohammadpourLog_warning.txt"
                , minimumLevel: LogLevel.Warning);
            loggerFactory.AddFile($@"D:\MohammadpourLog_error.txt"
                , minimumLevel: LogLevel.Error);
            loggerFactory.AddFile($@"D:\MohammadpourLog_critical.txt"
                , minimumLevel: LogLevel.Critical);

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

            //app.UseAuthorization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            InitIdentity(userManager, roleManager).Wait();
        }

        private async Task InitIdentity(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new List<string> {
                "ادمین","فروشنده","خریدار"
            };
            foreach (var item in roles)
            {
                if ((await roleManager.RoleExistsAsync(item)) == false)
                {
                    var role = new IdentityRole(item);
                    await roleManager.CreateAsync(role);
                }
            }
            var useradmin = await userManager.FindByNameAsync("admin@gmail.com");
            if (useradmin == null)
            {
                useradmin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    firstname = "ادمین"
                };
                await userManager.CreateAsync(useradmin,"pP=-0987");
                await userManager.AddToRoleAsync(useradmin,"ادمین");
            }

        }
    }
}
