using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using MerchantPortal.Areas.MasterSetup.Models;
using MerchantPortal.Models;

namespace MerchantPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Int64>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<MerchantPortal.Areas.MasterSetup.Models.District> MSDistrict { get; set; }    

        public DbSet<MerchantPortal.Models.DistrictSV> DistrictSV { get; set; }    
       // public DbSet<MerchantPortal.Data.Models.Agent> Agent { get; set; }
    }
}
