using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RestaurantContext, Restaurant.Migrations.Configuration>());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Staff>  Staff { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<tblMain> TableMain { get; set; }
        public DbSet<tblDetails> TableDetails { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
