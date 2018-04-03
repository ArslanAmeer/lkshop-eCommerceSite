using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.ProductMgmt
{
    public class ProductHandler
    {
        private DBContextClass db = new DBContextClass();
        public Camera GetCamera(int? id)
        {
            using (db)
            {
                return (from c in db.Cameras
                            .Include("Series.Brand")
                            .Include("SubCategory.Category")
                            .Include("Images")
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }

        public List<Camera> GetLatestCameras(int numb)
        {
            using (db)
            {
                return (from c in db.Cameras
                            .Include("Series.Brand")
                            .Include("SubCategory.Category")
                            .Include("Images")
                        orderby c.AnnounceDate descending
                        select c).Take(numb).ToList();
            }
        }

        public List<Camera> GetAllCameras()
        {
            using (db)
            {
                return (from c in db.Cameras
                            .Include("Series.Brand")
                            .Include("SubCategory.Category")
                            .Include("Images")
                        select c).ToList();
            }
        }

        public List<Brand> GetBrands()
        {
            using (db)
            {
                return (from c in db.Brands select c).ToList();
            }
        }

        public Brand BrandById(int id)
        {
            using (db)
            {
                return (from b in db.Brands where b.Id == id select b).FirstOrDefault();
            }
        }

        public List<Series> GetSeries(Brand brand)
        {
            using (db)
            {
                return (from s in db.Series_
                        where s.Brand.Id == brand.Id
                        select s).ToList();
            }
        }

        public List<Category> GetCategories()
        {
            using (db)
            {
                return (from c in db.Categories select c).ToList();
            }
        }

        public List<SubCategory> GetSubCategories(Category category)
        {
            using (db)
            {
                return (from s in db.SubCategories
                        where s.Category.Id == category.Id
                        select s).ToList();
            }
        }

        public void AddCamera(Camera camera)
        {
            using (db)
            {
                db.Entry(camera.Brand).State = EntityState.Unchanged;
                db.Entry(camera.Series).State = EntityState.Unchanged;
                db.Entry(camera.Category).State = EntityState.Unchanged;
                db.Entry(camera.SubCategory).State = EntityState.Unchanged;
                db.Cameras.Add(camera);
                db.SaveChanges();
            }
        }

        public void DeleteCamera(int id)
        {
            using (db)
            {
                //Camera c = db.Cameras.Find(id);
                db.Cameras.Remove(GetCamera(id));
                db.SaveChanges();
            }
        }

        public void UpdateCamera(Camera camera)
        {
            using (db)
            {
                //db.Entry(camera.Brand).State = EntityState.Unchanged;
                //db.Entry(camera.Series).State = EntityState.Unchanged;
                //db.Entry(camera.Category).State = EntityState.Unchanged;
                //db.Entry(camera.SubCategory).State = EntityState.Unchanged;
                db.Entry(camera).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Camera> GetCameraByBrands(Brand brand)
        {
            using (db)
            {
                return (from c in db.Cameras
                            .Include("Series.Brand")
                            .Include("SubCategory.Category")
                            .Include("Images")
                        where c.Brand.Id == brand.Id
                        select c).ToList();
            }
        }

        public int GetProductCount()
        {
            using (db)
            {
                return (from c in db.Cameras select c).Count();
            }

        }

    }
}
