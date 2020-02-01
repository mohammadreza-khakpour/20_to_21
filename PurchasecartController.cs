using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Models;

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
        
        public string ComputeTotalSum(int purchasecartId)
        {
            var totalSum  =
                db.PurchasecartProducts.Where(x => x.PurchasecartId == purchasecartId)
                .Include(x => x.Product).Sum(x => x.count * x.Product.price);
            return $"تومان {totalSum:0,0} ";
        }
        public IActionResult ChangeCount(int count, int Id)
        {
            var purchasecartProduct = db.Find<PurchasecartProduct>(Id);
            purchasecartProduct.count = count;
            var product = db.Find<Product>(purchasecartProduct.ProductId);
            db.SaveChanges();
            return Json(new
            {
                totalSumItem = $"تومان {(purchasecartProduct.count * product.price):0,0}"
                ,
                totalSumInvoice = ComputeTotalSum(purchasecartProduct.PurchasecartId)
            });
        }

        public IActionResult RemoveProductItems(List<int> Ids)
        {
            var p = db.PurchasecartProducts.Where(x => Ids.Contains(x.Id));
            db.RemoveRange(p);
            db.SaveChanges();
            return Json(true);
        }


        public async Task<IActionResult> Index()
        {
            var userId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;

            var purchasecart =
                db.Purchasecarts.FirstOrDefault(x => x.UserId == userId && !x.isPaid);

            var purchaseproducts =
                db.PurchasecartProducts.Where(x => x.PurchasecartId == purchasecart.Id)
                .Include(x => x.Product).ToList();

            ViewData["totalsum"] = ComputeTotalSum(purchasecart.Id);

            return View(purchaseproducts);
        }


    }
}