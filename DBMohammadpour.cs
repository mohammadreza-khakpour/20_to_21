using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MohammadpourAspNetCoreSaturdayMondayEvening.Areas.Identity.Data;
using MohammadpourAspNetCoreSaturdayMondayEvening.Models;

namespace MohammadpourAspNetCoreSaturdayMondayEvening.Data
{
    public class DBMohammadpour : IdentityDbContext<ApplicationUser>
    {
        public DBMohammadpour(DbContextOptions<DBMohammadpour> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
