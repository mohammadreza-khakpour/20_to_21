using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Models
{
    public class Purchasecart
    {
        public int Id { get; set; }
        public bool isPaid { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public ICollection<PurchasecartProduct> PurchasecartProducts { get; set; }
    }
}
