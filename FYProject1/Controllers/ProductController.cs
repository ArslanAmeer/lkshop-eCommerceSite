using FYProject1.Models;
using FYProject1Classes;
using FYProject1Classes.ProductMgmt;
using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FYProject1Classes.UserMgmt;
using Brand = FYProject1Classes.ProductMgmt.Brand;

namespace FYProject1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductManagment()
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Admin", act = "AdminPanel" });
            }
            List<Camera> camera = new ProductHandler().GetAllCameras();
            ViewBag.productcount = new ProductHandler().GetProductCount();
            ViewBag.categories = ModelHelper.ToSelectItemList(new ProductHandler().GetCategories());
            return View(camera);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Product", act = "AddProduct" });
            }
            ViewBag.brands = ModelHelper.ToSelectItemList(new ProductHandler().GetBrands());
            ViewBag.categories = ModelHelper.ToSelectItemList(new ProductHandler().GetCategories());
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(FormCollection fdata)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Product", act = "AddProduct" });
            }

            Camera c = new Camera();

            c.Title = fdata["title"];
            c.Category = new Category { Id = Convert.ToInt32(fdata["Categories"]) };
            c.SubCategory = new SubCategory { Id = Convert.ToInt32(fdata["SubCategoryList"]) };
            c.Brand = new Brand { Id = Convert.ToInt32(fdata["Brands"]) };
            c.Series = new Series { Id = Convert.ToInt32(fdata["SeriesList"]) };
            c.Wifi = Convert.ToBoolean(fdata["wifi"].Split(',').First());
            c.Bluetooth = Convert.ToBoolean(fdata["bluetooth"].Split(',').First());
            c.GPS = Convert.ToBoolean(fdata["gps"].Split(',').First());
            c.ExtMic = Convert.ToBoolean(fdata["extmic"].Split(',').First());
            c.Level = Convert.ToString(fdata["level"]);
            c.MegaPixel = fdata["mp"];
            c.SensorFormat = Convert.ToString(fdata["sensorformat"]);
            c.LCDDetail = fdata["lcddetail"];
            c.LCDType = Convert.ToString(fdata["lcdtype"]);
            c.VFType = Convert.ToString(fdata["vftype"]);
            //c.AnnounceDate = Convert.ToString(fdata["ancdate"]);
            if (string.IsNullOrEmpty(fdata["ancdate"]))
            {
                c.AnnounceDate = null;
            }
            else
            {
                string[] dparts = fdata["ancdate"].Split('-');
                c.AnnounceDate = new DateTime(Convert.ToInt32(dparts[2]), Convert.ToInt32(dparts[1]), Convert.ToInt32(dparts[0]));
            }
            c.Price = Convert.ToInt64(fdata["price"]);
            c.Sale = Convert.ToInt32(fdata["sale"]);
            c.SensorType = Convert.ToString(fdata["sensortype"]);
            c.FocusSystem = fdata["focussys"];
            c.ImageProcessor = fdata["imagepro"];
            c.ISORange = fdata["isorange"];
            c.BurstShot = fdata["burstshot"];
            c.VideoRecording = fdata["vidrec"];
            c.ShutterSpeed = fdata["shuttspeed"];
            c.LensMount = fdata["lensmount"];
            c.Kit = fdata["kit"];
            c.BuiltinFlash = Convert.ToBoolean(fdata["bflash"].Split(',').First());
            c.WeatherSeal = Convert.ToBoolean(fdata["wseal"].Split(',').First());
            c.cardslots = Convert.ToBoolean(fdata["mcards"].Split(',').First());
            c.Description = Convert.ToString(fdata["txtEditor"].Split(',').First());
            long numb = DateTime.Now.Ticks;
            int count = 0;
            //foreach (string fname in Request.Files)
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    string name = file.FileName;
                    string url = "/ImagesData/CameraImages/" + numb + "_" + ++count + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string path = Request.MapPath(url);
                    file.SaveAs(path);
                    c.Images.Add(new CameraImages { Caption = name, Image_Url = url });
                }
                else
                {
                    string name = "No Image";
                    string url = "/ImagesData/noimage2.jpg";
                    c.Images.Add(new CameraImages { Caption = name, Image_Url = url });
                }
            }

            new ProductHandler().AddCamera(c);
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return RedirectToAction("ProductManagment");

        }

        [HttpGet]
        public ActionResult SeriesList(int id)
        {
            DDViewModel dm = new DDViewModel();
            dm.Name = "SeriesList";
            dm.Label = "- Series -";
            dm.Values = ModelHelper.ToSelectItemList(new ProductHandler().GetSeries(new Brand { Id = id }));

            return PartialView("~/Views/Shared/_DDLViewBoot.cshtml", dm);
        }

        [HttpGet]
        public ActionResult SubCategoryList(int id)
        {
            DDViewModel dm = new DDViewModel();
            dm.Name = "SubCategoryList";
            dm.Label = "- Sub Category -";
            dm.Values = ModelHelper.ToSelectItemList(new ProductHandler().GetSubCategories(new Category { Id = id }));

            return PartialView("~/Views/Shared/_DDLViewBoot.cshtml", dm);
        }

        public ActionResult ProductDetails(int? id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Home", act = "Index" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera cam = new ProductHandler().GetCamera(id);
            if (cam == null)
            {
                return HttpNotFound();
            }
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return View(cam);
        }

        public ActionResult EditProduct(int id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Product", act = "EditProduct" });
            }

            Camera cam = new ProductHandler().GetCamera(id);
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return View(cam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Camera camera)
        {
            if (ModelState.IsValid)
            {
                new ProductHandler().UpdateCamera(camera);
                return RedirectToAction("ProductManagment");
            }
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return View(camera);
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camera camera = new ProductHandler().GetCamera(id);
            if (camera == null)
            {
                return HttpNotFound();
            }
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return View(camera);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Product", act = "ProductManagment" });
            }
            new ProductHandler().DeleteCamera(id);
            ViewBag.productcount = new ProductHandler().GetProductCount();
            return RedirectToAction("ProductManagment");
        }

        public ActionResult SuperDeal()
        {
            // Passing all Brands from Database to MAIN-LAYOUT
            List<Brand> brands = new List<Brand>();
            brands = new ProductHandler().GetBrands();
            ViewBag.brands = brands;

            // Passing Category List from Database to MAIN-LAYOUT

            List<Category> categories = new List<Category>();
            categories = new ProductHandler().GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            DBContextClass db = new DBContextClass();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}