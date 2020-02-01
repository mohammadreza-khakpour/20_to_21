using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Controllers
{
    public class PurchasecartController : Controller
    {
        DBMohammadpour db;
        UserManager<ApplicationUser> userManager;
        public PurchasecartController(DBMohammadpour _db,
                                UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
            var purchasecart =
                db.Purchasecarts.FirstOrDefault(x => x.UserId == userId && !x.isPaid);
            var purchaseproducts =
                db.PurchasecartProducts.Where(x => x.PurchasecartId == purchasecart.Id)
                .Include(x => x.Product).ToList();
            return View(purchaseproducts);
        }
    }
}