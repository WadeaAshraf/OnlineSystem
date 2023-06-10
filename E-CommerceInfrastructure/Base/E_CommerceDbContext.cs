using E_CommerceCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceInfrastructure.Base
{
    public class E_CommerceDbContext:DbContext
    {
        public E_CommerceDbContext(DbContextOptions<E_CommerceDbContext> options)
           : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories
                {
                    Id = 1,
                    Name = "Moblie",
                    CreationDate = DateTimeOffset.Now
                },
                new Categories
                {
                    Id = 2,
                    Name = "LapTop",
                    CreationDate = DateTimeOffset.Now
                }

            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Server=DESKTOP-3FRRSPF;Database=E_Commerce;Trusted_Connection=True;");
        }
    }
}
