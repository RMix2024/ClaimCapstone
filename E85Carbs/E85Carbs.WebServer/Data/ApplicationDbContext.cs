using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E85Carbs.WebServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<ProductGalleryImage> productGalleryImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public List<IFormFile> File { get; internal set; }
        public DbSet<CustomerBuild> customerBuilds { get; set; }
        public DbSet<BuildGalleryImage> buildGalleryImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(200));
            //modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(200));
            //modelBuilder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(200));


            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(200));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(200));


            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(200));


            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));


            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(200));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));

            modelBuilder.Entity<Product>().Property(p => p.MainProductImage).HasColumnType("MediumBlob");
            modelBuilder.Entity<ProductGalleryImage>().Property(p => p.ProductGalleryByte).HasColumnType("MediumBlob");
            modelBuilder.Entity<CustomerBuild>().Property(p => p.MainBuildImage).HasColumnType("MediumBlob");
            modelBuilder.Entity<BuildGalleryImage>().Property(p => p.BuildGalleryByte).HasColumnType("MediumBlob");



            modelBuilder.Entity<IdentityRole>().HasData
                (
                new IdentityRole { 
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR".ToUpper() 
                });

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "eurotuner1981@gmail.com",
                NormalizedUserName = "EUROTUNER1981@GMAIL.COM",
                Email = "eurotuner1981@gmail.com",
                NormalizedEmail = "EUROTUNER1981@GMAIL.COM".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rdpA44W0RD")
                }
            );


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(
                new IdentityRoleClaim<string>
                {
                    Id = 1,
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    ClaimType = "AdminOnly",
                    ClaimValue = "AdminOnly"
               
                }
            );

            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    ClaimType = "AdminOnly",
                    ClaimValue = "AdminOnly"

                }
            );




        }

    }
}

