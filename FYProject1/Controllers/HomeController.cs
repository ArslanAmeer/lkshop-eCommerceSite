using FYProject1.Models;
using FYProject1Classes;
using FYProject1Classes.BannerMgmt;
using FYProject1Classes.ProductMgmt;
using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYProject1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Passing all banners from database to MAIN PAGE

            List<MainBanner> banners = new BannersHandler().GetAllBanners();
            ViewBag.banners = banners;

            // Geting Products Summary from Database to Summary model to Index page

            ViewBag.indexProducts = ModelHelper.ToCameraSummaryList(new ProductHandler().GetLatestCameras(12));


            return View();
        }

        public ActionResult ProductsByBrand(int id)
        {
            ViewBag.CameraByBrands = ModelHelper.ToCameraSummaryList(new ProductHandler().GetCameraByBrands(new Brand { Id = id }));
            ViewBag.brand = new ProductHandler().BrandById(id);
            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            Camera c = new ProductHandler().GetCamera(id);
            return View(c);
        }

        public ActionResult OrderTrack()
        {

            return View();
        }

        [HttpPost]
        public ActionResult OrderTrack(string orderNumb)
        {

            return View();
        }
    }
}