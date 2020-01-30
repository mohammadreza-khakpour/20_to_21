using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.ViewModels;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Controllers
{
    public class AccountController : Controller
    {
        //Dependency Injection

        UserManager<ApplicationUser> userManager { get; set; }
        SignInManager<ApplicationUser> signManager { get; set; }

        public AccountController(UserManager<ApplicationUser> _userManager,
                                 SignInManager<ApplicationUser> _signManager)
        {
            userManager = _userManager;
            signManager = _signManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.username);
            if (user != null)
            {
                var status = await signManager.PasswordSignInAsync(user, model.password, model.rememberMe, true);
                if (status.Succeeded)
                {
                    //User.Identity.IsAuthenticated
                    //User.Identity.Name
                    return RedirectToAction("Index","Home");
                }
                else if (status.IsLockedOut)
                {

                }
                else if (status.IsNotAllowed)
                {

                }
            }
            else
            {
                //Message
                return RedirectToAction("RegisterLogin");
            }
            return RedirectToAction("RegisterLogin");
        }


        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            //Asyncronously
            ApplicationUser newuser = new ApplicationUser()
            {
                Email = model.username,
                UserName = model.username,
                firstname = model.firstname,
                lastname = model.lastname,
                //EmailConfirmed = true
            };
            IdentityResult status = await userManager.CreateAsync(newuser, model.password);

            if (status.Succeeded == true)
            {
                //TempData
            }
            else
            {
                //status.Errors
            }
            //userManager.CreateAsync(newuser, model.password);
            return View("RegisterLogin");
        }

        public IActionResult RegisterLogin()
        {
            return View();
        }
    }
}