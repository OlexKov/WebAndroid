using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAndroid.Data.Entities;

namespace WebAndroid.Data
{
    public class AndroidDbContext(DbContextOptions opt) : IdentityDbContext<EntityUser, IdentityRole<int>, int> (opt)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<EntityUser> EntityUsers { get; set; }
        public DbSet<ProductImageEntity> ProductImages { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
    }
}
