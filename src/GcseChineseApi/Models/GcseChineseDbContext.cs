using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace GcseChineseApi.Models
{
    public class GcseChineseDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<Exampaper> Exampapers { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Resource> Resources { get; set; } 

        public GcseChineseDbContext()
        {
            Database.Migrate();
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Assessment>().HasMany(p => p.Exampapers).WithOne();
            builder.Entity<Exampaper>().HasOne(a => a.Assessment).WithMany(p => p.Exampapers);
        }
    }
}
