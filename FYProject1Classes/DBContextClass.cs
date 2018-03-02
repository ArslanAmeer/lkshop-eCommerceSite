using FYProject1Classes.LocationMgmt;
using FYProject1Classes.ProductMgmt;
using FYProject1Classes.BannerMgmt;
using System.Data.Entity;
using FYProject1Classes.CartManagment;
using FYProject1Classes.FinalOrderMgmt;
using FYProject1Classes.UserMgmt;

namespace FYProject1Classes
{
    public class DBContextClass : DbContext
    {
        public DBContextClass() : base("DBconstr")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Series> Series_ { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<MainBanner> MainBanners { get; set; }


        public DbSet<FinalOrder> FinalOrders { get; set; }

    }
}
