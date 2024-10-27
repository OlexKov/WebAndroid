using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAndroid.Data.Entities;

namespace WebAndroid.Data
{
    public class AndroidDbContext(DbContextOptions opt) : IdentityDbContext<EntityUser, IdentityRole<int>, int> (opt)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<EntityUser> EntityUsers { get; set; }
    }
}
