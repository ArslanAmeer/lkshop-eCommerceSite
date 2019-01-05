using FYProject1.Models;
using FYProject1Classes;
using FYProject1Classes.LocationMgmt;
using FYProject1Classes.UserMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FYProject1Classes.BannerMgmt;
using FYProject1Classes.ProductMgmt;

namespace FYProject1.Controllers
{
    public class UserController : Controller
    {
        private readonly DBContextClass _db = new DBContextClass();

        [HttpGet]
        public ActionResult Login()
        {
            HttpCookie myCookie = Request.Cookies["idpas"];
            if (myCookie != null)
            {
                User u = new UserHandler().GetUser(myCookie.Values["lid"], myCookie.Values["psd"]);
                if (u != null)
                {
                    myCookie.Expires = DateTime.Today.AddDays(7);
                    Response.SetCookie(myCookie);

                    Session.Add(WebUtil.CURRENT_USER, u);

                    if (u.IsInRole(WebUtil.ADMIN_ROLE))
                    {
                        return RedirectToAction("AdminPanel", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Controller = Request.QueryString["ctl"];
            ViewBag.Action = Request.QueryString["act"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User u = new UserHandler().GetUser(data.LoginId, data.Password);
            if (u != null)
            {
                if (data.RememberMe)
                {
                    HttpCookie c = new HttpCookie("idpas")
                    {
                        Expires = DateTime.Today.AddDays(7),
                    };
                    c.Values.Add("lid", u.LoginID);
                    c.Values.Add("psd", u.Password);
                    Response.SetCookie(c);
                }


                Session.Add(WebUtil.CURRENT_USER, u);

                string ctl = Request.QueryString["c"];
                string act = Request.QueryString["a"];

                if (!string.IsNullOrEmpty(ctl) && !string.IsNullOrEmpty(act))
                {
                    return RedirectToAction(act, ctl);
                }
                else if (u.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("AdminPanel", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.failed = "Invalid Username or Password! Please Try Again or ";
            }
            return View();
        }

        public ActionResult Logout()
        {
            ActionResult obj;

            User u = (User)Session[WebUtil.CURRENT_USER];
            if (u != null && u.IsInRole(WebUtil.ADMIN_ROLE))
            {
                obj = RedirectToAction("Login", "User");
            }
            else
            {
                obj = RedirectToAction("Index", "Home");
            }

            Session.Abandon();
            HttpCookie ck = Request.Cookies["idpas"];
            if (ck != null)
            {
                ck.Expires = DateTime.Now;
                Response.SetCookie(ck);
            }
            return obj;
        }

        [HttpGet]
        public ActionResult SignUp()
        {

            LocationHandler lh = new LocationHandler();
            ViewBag.CountryList = ModelHelper.ToSelectItemList(lh.GetCountries());

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(FormCollection fdata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User u = new User
                    {
                        FullName = fdata["FullName"],
                        LoginID = fdata["ConfirmEmail"],
                        Password = fdata["ConfirmPassword"],
                        Email = fdata["ConfirmEmail"],
                        Occupation = fdata["occu"],
                        FullAddress = fdata["FullAddress"],
                        Phone = Convert.ToInt64(fdata["Phone"]),
                        SecurityQuestion = Convert.ToString(fdata["secqueslist"]),
                        SecurityAnswer = fdata["secans"],
                        IsActive = Convert.ToBoolean(fdata["isactive"].Split(',').First()),
                        CityId = new City { Id = Convert.ToInt32(fdata["CityList"]) }
                    };

                    if (string.IsNullOrEmpty(fdata["DOB"]))
                    {
                        u.BirthDate = null;
                    }
                    else
                    {
                        string[] dParts = fdata["DOB"].Split('/');
                        u.BirthDate = new DateTime(Convert.ToInt32(dParts[2]), Convert.ToInt32(dParts[1]), Convert.ToInt32(dParts[0]));
                    }

                    string gender = Convert.ToString(fdata["Gender"]);
                    if (gender != null && gender == "Male")
                    {
                        u.Male = true;
                        u.Female = false;
                    }
                    else if (gender != null && gender == "Female")
                    {
                        u.Male = false;
                        u.Female = true;
                    }

                    u.Role = new UserHandler().GetRoles(2);

                    long numb = DateTime.Now.Ticks;
                    int count = 0;
                    foreach (string fname in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fname];
                        if (!string.IsNullOrEmpty(file?.FileName))
                        {
                            string name = file.FileName;
                            string url = "/ImagesData/UserImages/" + numb + "_" + ++count + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal));
                            string path = Request.MapPath(url);
                            file.SaveAs(path);
                            u.UserImage.Add(new UserImage { Url = url, Priority = count, Caption = name });
                        }
                        else
                        {
                            string name = "No Image";
                            string url = "/ImagesData/noimage.jpg";
                            u.UserImage.Add(new UserImage { Url = url, Priority = 0, Caption = name });
                        }
                    }

                    new UserHandler().Adduser(u);
                    return RedirectToAction("Login");
                }

                return RedirectToAction("SignUp");
            }
            catch (Exception)
            {
                return RedirectToAction("SignUp");
            }


        }

        [HttpGet]
        public ActionResult CityLists(int id)
        {
            DDViewModel dm = new DDViewModel
            {
                Name = "CityList",
                Label = "- Your City -",
                Values = ModelHelper.ToSelectItemList(new LocationHandler().GetCities(new Country { Id = id }))
            };

            return PartialView("~/Views/Shared/_DDLView.cshtml", dm);
        }

        [HttpGet]
        public ActionResult UserManagment()
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Home", act = "Index" });
            }

            List<User> users = new UserHandler().GetUsers();
            ViewBag.roles = ModelHelper.ToSelectItemList(new UserHandler().GetRoles());
            return View(users);
        }

        [HttpGet]
        public ActionResult UserDetails(int? id)
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
            User user = new UserHandler().GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult UserGuestUpdate(int? id)
        {
            LocationHandler lh = new LocationHandler();
            ViewBag.CountryList = ModelHelper.ToSelectItemList(lh.GetCountries());

            User u = (User)Session[WebUtil.CURRENT_USER];
            if (u != null && u.IsInRole(WebUtil.ADMIN_ROLE))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("UserEdit", new { id = u.Id });
            }
            else if (u != null && !(u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = new UserHandler().GetUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UserGuestUpdate(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new UserHandler().UpdateUser(user);
                    Session.Add(WebUtil.CURRENT_USER, new UserHandler().GetUser(user.Id));
                }

                ViewBag.msg = "Update Successfully";
            }
            catch (Exception e)
            {
                ViewBag.msg = "Failed To Update! (" + e.Message + ")";
            }

            return View(user);
        }

        public ActionResult UserEdit(int? id)
        {
            LocationHandler lh = new LocationHandler();
            ViewBag.CountryList = ModelHelper.ToSelectItemList(lh.GetCountries());
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Home", act = "Index" });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = new UserHandler().GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(User user, FormCollection fdata)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Home", act = "Index" });
            }

            if (ModelState.IsValid)
            {
                user.CityId = new City { Id = Convert.ToInt32(fdata["CityList"]) };
                user.Role = new Role { Id = Convert.ToInt32(fdata["Role.Id"]) };
                new UserHandler().UpdateUserByAdmin(user);
                return RedirectToAction("UserManagment");
            }
            return View(user);
        }

        public ActionResult UserDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = new UserHandler().GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(int id)
        {
            User u = (User)Session[WebUtil.CURRENT_USER];
            if (!(u != null && u.IsInRole(WebUtil.ADMIN_ROLE)))
            {
                return RedirectToAction("Login", "User", new { ctl = "Home", act = "Index" });
            }
            User user = new UserHandler().GetUser(id);
            foreach (var i in user.UserImage)
            {
                string path = Request.MapPath(i.Url);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            new UserHandler().DeleteUser(id);
            return RedirectToAction("UserManagment");
        }

        public int UserCount()
        {
            return new UserHandler().GetUserCount();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}