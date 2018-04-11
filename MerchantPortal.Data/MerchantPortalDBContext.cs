using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MerchantPortal.Data
{
    public class MerchantPortalDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Int64, IdentityUserClaim<Int64>, UsmUserRole, IdentityUserLogin<Int64>, IdentityRoleClaim<Int64>, IdentityUserToken<Int64>>
    {

        public MerchantPortalDBContext(DbContextOptions<MerchantPortalDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Usm_Users");
            builder.Entity<ApplicationRole>().ToTable("Usm_Roles");
            builder.Entity<IdentityUserClaim<Int64>>().ToTable("Usm_UserClaim");
            builder.Entity<UsmUserRole>().ToTable("Usm_UserRole");
            builder.Entity<IdentityUserLogin<Int64>>().ToTable("Usm_UserLogin");
            builder.Entity<IdentityRoleClaim<Int64>>().ToTable("Usm_RoleClaim");
            builder.Entity<IdentityUserToken<Int64>>().ToTable("Usm_UserToken");

            builder.Entity<Menu>().ToTable("Usm_Menu");
            builder.Entity<ControllerActionMapping>().ToTable("Usm_ControllerActionMapping");
            builder.Entity<RolePermission>().ToTable("Usm_RolePermission");
        }

        public DbSet<Agent> Agent { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Terminal> Terminal { get; set; }
        public DbSet<MctCommissionSetup> MctCommissionSetup { get; set; }
        public DbSet<MctVATSetup> MctVATSetup { get; set; }
        public DbSet<MctGLSetup> MctGLSetup { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApprovalViewModel> ApprovalViewModel { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<SettlementRule> SettlementRule { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<ControllerActionMapping> ControllerActionMapping { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<Menu> Menu { get; set; }

    }
}
