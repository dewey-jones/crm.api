using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace crmApi.Models
{
    public class CrmContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public CrmContext(DbContextOptions<CrmContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Zipcode> Zipcodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // For Guid Primary Key
            builder.Entity<ApplicationUser>().Property(p => p.Id).ValueGeneratedOnAdd();

            // For int Primary Key
            //builder.Entity<ApplicationUser>().Property(p => p.Id).UseSqlServerIdentityColumn();
        }
    }
}
