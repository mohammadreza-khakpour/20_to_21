using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.ViewModels
{
    public class RegisterViewModel
    {
        public string username { get; set; }
        
        public string password { get; set; }
        [Compare("password",ErrorMessage ="")]
        public string repassword { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}
