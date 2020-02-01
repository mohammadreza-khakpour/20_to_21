using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MohammadpourAspNetCoreSaturdayMondayEvening.Models;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }

        public ICollection<Purchasecart> Purchasecarts { get; set; }
    }
}
