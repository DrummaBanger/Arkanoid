using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<RecordsData> Records { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RecordsData>().HasKey(m => m.RecordID);
            base.OnModelCreating(builder);
        }
    }
}
