using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Models
{
    public class PurchasecartProduct
    {
        public int Id { get; set; }
        public int count { get; set; }

        public int PurchasecartId { get; set; }
        [ForeignKey("PurchasecartId")]
        public Purchasecart Purchasecart { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


    }
}
