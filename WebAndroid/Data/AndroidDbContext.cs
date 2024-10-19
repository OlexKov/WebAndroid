using Microsoft.EntityFrameworkCore;
using WebAndroid.Data.Entities;

namespace WebAndroid.Data
{
    public class AndroidDbContext(DbContextOptions opt) : DbContext(opt)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }
    }
}
