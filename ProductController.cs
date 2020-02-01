using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Models;
using MohammadpourAspNetCoreSaturdayMondayEvening.ViewModels;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Controllers
{
    public class ProductController : Controller
    {
        DBMohammadpour db;
        UserManager<ApplicationUser> userManager;
        public ProductController(DBMohammadpour _db,
                                UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
            var purchasecart =
                db.Purchasecarts.FirstOrDefault(x => x.UserId == userId && !x.isPaid);
            if (purchasecart == null)
            {
                purchasecart = new Purchasecart() { UserId = userId, isPaid = false };
                db.Add(purchasecart);
                db.SaveChanges();
            }
            if (db.PurchasecartProducts.Any(x => x.PurchasecartId == purchasecart.Id &&
                                            x.ProductId == productId) == false)
            {
                PurchasecartProduct purchasecartProduct = new PurchasecartProduct
                {
                    ProductId = productId,
                    PurchasecartId = purchasecart.Id,
                    count = 1
                };
                db.Add(purchasecartProduct);
                db.SaveChanges();
            }
            return Json(true);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult InsertConfirm(ProductViewModel model)
        {
            var product = new Product
            {
                count = model.count,
                name = model.name,
                price = model.price
            };
            if (model.img != null)
            {
                if (model.img.Length <= 5 * Math.Pow(1024, 2) &&
                   model.img.Length >= 2 * Math.Pow(1024, 1))
                {
                    if (Path.GetExtension(model.img.FileName).ToLower() == ".jpg")
                    {
                        byte[] b = new byte[model.img.Length];
                        try
                        {
                            model.img.OpenReadStream().Read(b, 0, b.Length);
                            product.img = b;
                            MemoryStream memoryStream = new MemoryStream(b);
                            Image image = Image.FromStream(memoryStream); //img.Width,img.Height
                            Bitmap bitmap = new Bitmap(image, 700, 700);// 200, 200 * image.Height / image.Width);
                            MemoryStream memoryStreamThumbnail = new MemoryStream();
                            bitmap.Save(memoryStreamThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                            product.imgThumbnail = memoryStreamThumbnail.ToArray();
                            memoryStream.Dispose();
                            memoryStreamThumbnail.Dispose();
                        }
                        catch { }
                    }
                }
            }
            db.Add(product);
            db.SaveChanges();
            return RedirectToAction("Insert");
        }


        public IActionResult F1() => View();

        public IActionResult ShowOriginalImage(int Id)
        {
            byte[] b = db.Find<Product>(Id).img;
            return File(b, "image/jpeg", "Image.jpeg");
            //return File(b, "image/jpeg","Image.jpeg");
        }
    }

}
