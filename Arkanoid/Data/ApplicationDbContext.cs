using System;
using System.Collections.Generic;
using System.Text;
using Arkanoid.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Arkanoid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Records> Records { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Records>().HasKey(m => m.RecordID);
            base.OnModelCreating(builder);
        }
    }
}
