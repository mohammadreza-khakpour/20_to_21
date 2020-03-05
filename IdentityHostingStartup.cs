using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;

[assembly: HostingStartup(typeof(MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.IdentityHostingStartup))]
namespace MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<DBMohammadpour>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DBMohammadpourConnection"))
                    );

                services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DBMohammadpour>();

                services.Configure<IdentityOptions>(x =>
                {
                    x.Lockout.MaxFailedAccessAttempts = 5;
                    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

                    x.Password.RequiredLength = 3;
                    x.Password.RequireDigit = false;
                    x.Password.RequireNonAlphanumeric = false; //symbol
                    x.Password.RequireLowercase = false;
                    x.Password.RequireUppercase = false;
                });


                services.ConfigureApplicationCookie(x =>
                {
                    x.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = y =>
                        {
                            y.Response.Redirect("/Account/RegisterLogin");
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = y =>
                        {
                            y.Response.Redirect("/Account/RegisterLogin");
                            return Task.CompletedTask;
                        }
                    };
                });

                services.AddAuthorization(x =>
                {
                    x.AddPolicy("Adminpolicy", x => x.RequireRole("ادمین"));

                    //x.AddPolicy("adminpolicy", x => x.RequireRole("ادمین").RequireRole("فروشنده")); AND
                    
                    //x.AddPolicy("adminpolicy", x => x.RequireRole("ادمین"));  //OR
                    //x.AddPolicy("adminpolicy", x => x.RequireRole("فروشنده")); //OR
                });

            });
        }
    }
}