using System;
using System.Web.Mvc;
using FYProject1.Models;
using System.Net.Mail;
using FYProject1Classes.LocationMgmt;
using System.Web.Security;
using System.IO;
using FYProject1Classes.UserMgmt;

namespace FYProject1.Controllers
{
    public class RecoveryEmailController : Controller
    {
        // GET: RecoveryEmail
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(recoveryEmail data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new UserHandler().GetUserByEmail(data.Email);
                    if (user==null)
                    {
                        ViewBag.error = "Email Not Registered. Please Enter Registered Email Address";
                        return View();
                    }

                    string randomnumb = Path.GetRandomFileName().Replace(".", "");                

                    var message = new MailMessage();
                    message.To.Add(new MailAddress(data.Email));
                    message.Subject = "-No-Reply- Password Recovery Email by [LK'- SHOP]";
                    message.Body = "Please use this password: "+randomnumb+" , Next Time You Login! And dont forget to change your password";
                    message.IsBodyHtml = false;
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        user.Password = randomnumb;
                        new UserHandler().UpdateUser(user);
                        ViewBag.success = "Email Has been sent to  " + data.Email;
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.error = "Error Sending Mail. Please Try Again Later!";
            }
            
            return View();
        }
    }
}