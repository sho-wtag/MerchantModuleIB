using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MerchantPortal.Areas.MasterSetup.Models;

namespace MerchantPortal.Areas.MasterSetup.Data
{
    public class MSDbContext : DbContext
    {
        public MSDbContext (DbContextOptions<MSDbContext> options)
            : base(options)
        {
        }

        public DbSet<MerchantPortal.Areas.MasterSetup.Models.District> MSDistrict { get; set; }
    }
}
