using FYProject1Classes;
using FYProject1Classes.BannerMgmt;
using FYProject1Classes.ProductMgmt;
using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FYProject1Classes.UserMgmt;

namespace FYProject1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminPanel()
        {
            DBContextClass db = new DBContextClass();
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Admin", act = "AdminPanel" });
            }
            return View();
        }

        [HttpGet]
        public ActionResult BannerManagment()
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User");
            }

            List<MainBanner> banners = new BannersHandler().GetAllBanners();
            return View(banners);

        }

        public ActionResult AddBanner()
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddBanner(FormCollection fdata)
        {
            MainBanner b = new MainBanner();
            try
            {
                long numb = DateTime.Now.Ticks;
                int count = 0;
                foreach (string fname in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fname];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        b.Caption = Convert.ToString(fdata["caption"]);
                        b.Banner_Url = "/ImagesData/MainPageBanners/" + file.FileName + numb + "_" + ++count + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        string path = Request.MapPath(b.Banner_Url);
                        if (file != null)
                        {
                            file.SaveAs(path);
                        }
                        new BannersHandler().AddBanner(b);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("BannerManagment");
        }

        [HttpGet]
        public ActionResult BannerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MainBanner banner = new BannersHandler().Getbanner(id);

            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        [HttpGet]
        public ActionResult BannerEdit(int? id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainBanner banner = new BannersHandler().Getbanner(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        [ValidateAntiForgeryToken]
        public ActionResult BannerEdit(MainBanner Banner)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                //long numb = DateTime.Now.Ticks;
                //int count = 0;
                //foreach (string fname in Request.Files)
                //{
                //    HttpPostedFileBase file = Request.Files[fname];
                //    if (!string.IsNullOrEmpty(file.FileName))
                //    {
                //        Banner.Banner_Url = "/ImagesData/MainPageBanners/" + file.FileName + numb + "_" + ++count + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                //        string path = Request.MapPath(Banner.Banner_Url);
                //        file.SaveAs(path);
                //    }

                //}
                new BannersHandler().UpdateBanner(Banner);
                return RedirectToAction("BannerManagment");
            }
            ViewBag.bannercount = new BannersHandler().GetBannerCount();
            return View();
        }

        public ActionResult BannerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MainBanner banner = new BannersHandler().Getbanner(id);

            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        [HttpPost, ActionName("BannerDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult BannerDeleteConfirmed(int id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User");
            }

            //Deleting IMAGE from both database and physical path

            MainBanner banner = new BannersHandler().Getbanner(id);

            string path = Request.MapPath(banner.Banner_Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                new BannersHandler().DeleteBanner(id);
            }
            return RedirectToAction("BannerManagment");
        }

        public int GetBannerCount()
        {
            return new BannersHandler().GetBannerCount();
        }

        //Garbage Colector and Disposing off Method
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