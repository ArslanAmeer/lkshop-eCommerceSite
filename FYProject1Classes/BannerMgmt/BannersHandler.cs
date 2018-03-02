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
        private DBContextClass db = new DBContextClass();

        public List<MainBanner> GetAllBanners()
        {
            using (db)
            {
                return (from b in db.MainBanners select b).ToList();
            }
        }

        public MainBanner Getbanner(int? id)
        {
            using (db)
            {
                return (from b in db.MainBanners where b.Id == id select b).FirstOrDefault();
            }
        }

        public void AddBanner(MainBanner banner)
        {
            using (db)
            {
                db.MainBanners.Add(banner);
                db.SaveChanges();
            }
        }

        public void UpdateBanner(MainBanner banner)
        {
            using (db)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteBanner(int id)
        {
            using (db)
            {                
                db.MainBanners.Remove(db.MainBanners.Find(id));
                db.SaveChanges();
            }
        }

        public int GetBannerCount()
        {
            using (db)
            {
                return (from c in db.MainBanners select c).Count();
            }
        }
    }
}
