using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        public async Task<IActionResult> Logout()
        {
            //User.Identity.Name
            await signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> ConfirmEmail(string id,string token)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            await userManager.ConfirmEmailAsync(user, token);
            TempData["msg"] = "ایمیل شما تاییذ گردیید";
            return RedirectToAction("RegisterLogin", "Account");
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
                
                //new{ a=1}  Anonymous Types
                string token = await userManager.GenerateEmailConfirmationTokenAsync(newuser);
                string href = Url.Action("ConfirmEmail", "Account", new
                {
                    id = newuser.Id,
                    token = token
                }, "https");
                string body = $"Hi, <b>{newuser.firstname} {newuser.lastname}</b><br/>" +
                               $"Click This <a href='{href}'>Link</a> to confirm you account.";

                MailMessage mailMessage = new MailMessage("mohammadpourmvc@gmail.com", newuser.Email);
                mailMessage.Subject = "Confirm Your Account";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //string s = System.IO.File.ReadAllText("d:\\password.txt");
                smtpClient.Credentials = new System.Net.NetworkCredential("mohammadpourmvc@gmail.com", "reza_1234567890");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                TempData["msg"] = "شما با موفقیت در سایت ثبن نام شده اید.ایمیل خود را چک نمایید";
                //MailServer
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