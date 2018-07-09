using FYProject1Classes.ProductMgmt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.BannerMgmt
{
    public class BannersHandler
    {
        private readonly DBContextClass _db = new DBContextClass();

        public List<MainBanner> GetAllBanners()
        {
            using (_db)
            {
                return (from b in _db.MainBanners select b).ToList();
            }
        }

        public MainBanner Getbanner(int? id)
        {
            using (_db)
            {
                return (from b in _db.MainBanners where b.Id == id select b).FirstOrDefault();
            }
        }

        public void AddBanner(MainBanner banner)
        {
            using (_db)
            {
                _db.MainBanners.Add(banner);
                _db.SaveChanges();
            }
        }

        public void UpdateBanner(MainBanner banner)
        {
            using (_db)
            {
                _db.Entry(banner).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void DeleteBanner(int id)
        {
            using (_db)
            {
                _db.MainBanners.Remove(_db.MainBanners.Find(id) ?? throw new InvalidOperationException());
                _db.SaveChanges();
            }
        }

        public int GetBannerCount()
        {
            using (_db)
            {
                return (from c in _db.MainBanners select c).Count();
            }
        }
    }
}
