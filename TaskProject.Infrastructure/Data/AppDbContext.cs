using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProject.Core.Entities;

namespace TaskProject.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext()
        {
                
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Product> Producst { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Entity<IdentityUser>().HasData(SeedUsers());

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(aa => aa.ProductDate).HasColumnType("date");
                entity.Property(aa => aa.Name).HasColumnType("nvarchar(100)");
                entity.Property(aa => aa.ManufacturePhone).HasColumnType("char(11)");
                entity.Property(aa => aa.ManufactureEmail).HasColumnType("char(50)");

                //entity.HasIndex(e => new { e.ProductDate, e.ManufactureEmail }, "ProductDate_ManufactureEmail_Unique").IsUnique();


                
            });

        }

        private static IdentityUser[] SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var usersDic = new Dictionary<string, string>()
            {
                {"vahid", "password" }
            };

            var users = usersDic.Select(u =>
            {
                var user = new IdentityUser(u.Key);
                user.NormalizedUserName = u.Key.ToUpper();
                var hashedPassword = hasher.HashPassword(user, u.Value);
                user.PasswordHash = hashedPassword;
                return user;
            }).ToArray();
            return users;
        }
    }
}
