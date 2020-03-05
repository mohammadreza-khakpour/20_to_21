using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MohammadpourAspNetCoreSaturdayMondayEvening.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Models;
using Token;
using Verify;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Controllers
{
    public class PaymentController : Controller
    {
        DBMohammadpour db;
        public PaymentController(DBMohammadpour _db) => db = _db;

        public int ComputeTotalSum(int purchasecartId) =>
                db.PurchasecartProducts.Where(x => x.PurchasecartId == purchasecartId)
                .Include(x => x.Product).Sum(x => x.count * x.Product.price);

        public async Task<IActionResult> Index(int purchaseCartId)
        {

            TokensClient client = new TokensClient();
            string money = ComputeTotalSum(purchaseCartId).ToString();
            tokenResponse tokenResp = await client.MakeTokenAsync(money
                , "", "",
                        /*"662584852"*/ purchaseCartId.ToString(), "",
                "https://localhost:44329/Payment/Verify", "Test Sample");

            ViewData["clientToken"] = tokenResp.token;
            ViewData["merchantId"] = "AA8B";
            ViewData["PaymentId"] = purchaseCartId.ToString();
            return View();
        }

        public async Task<ActionResult> Verify(string token, string merchantId, string resultCode
            , string paymentId, string referenceId)
        {
            int purchaseCartId = Convert.ToInt32(paymentId);
            // You should send this parameter to verifier service 
            var sha1Key = "";
            var VerifyService = new Verify.VerifyClient();
            {

                if (!string.IsNullOrEmpty(resultCode) && resultCode == "100")
                {
                    var res = await VerifyService.KicccPaymentsVerificationAsync(token, merchantId, referenceId, sha1Key);
                    if (res < 0)
                    {
                        db.Database.BeginTransaction();
                        try
                        {
                            db.Find<Purchasecart>(Convert.ToInt32(paymentId)).isPaid = true;
                            
                            db.PurchasecartProducts.Where(x => x.PurchasecartId == purchaseCartId)
                                .Include(x => x.Product).ToList().ForEach(x => x.Product.count -= x.count);

                            db.SaveChanges();
                            db.Database.CommitTransaction();
                        }
                        catch {
                            db.Database.RollbackTransaction();
                        }
                        //  Verification succed , your statements Goes here

                    }
                    else
                    {

                        //  Verification Failed , your statements Goes here

                    }
                }

            }

            return View("ProductList","Product");
        }

    }
}