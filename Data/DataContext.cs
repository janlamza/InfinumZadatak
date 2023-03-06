using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppContact>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<AppContact>()
            .HasIndex(b => b.Address)
            .IsUnique();
        }

        public DbSet<AppContact> AppContact { get; set; }
    }
}