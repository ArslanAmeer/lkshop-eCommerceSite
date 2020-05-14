
using FYProject1Classes;
using FYProject1Classes.CartManagment;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using FYProject1Classes.FinalOrderMgmt;
using FYProject1Classes.UserMgmt;

namespace FYProject1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public ActionResult ViewCart()
        {
            int total = 0;
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];

            if (cart != null && cart.NumberOfItems > 0)
            {
                var cartList = new List<ShoppingCartItem>();

                foreach (var c in cart.Items)
                {
                    ShoppingCartItem cartItem = new ShoppingCartItem
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageURL = c.ImageURL,
                        Sale = c.Sale
                    };
                    cartList.Add(cartItem);
                    cartList.TrimExcess();
                    total += c.Amount;
                }
                ViewBag.total = total;
                return View(cartList);
            }

            return View();
        }

        [HttpGet]
        public int AddToCart(ShoppingCartItem item)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART] ?? new ShoppingCart();

            cart.Add(item);
            Session.Add(WebUtil.CART, cart);

            return cart.NumberOfItems;
        }

        [HttpGet]
        public ActionResult RemoveFromCart(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart != null)
            {
                cart.Remove(id);
                Session.Add(WebUtil.CART, cart);
            }
            return RedirectToAction("ViewCart");
        }

        public int UpdateToCart()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart != null)
            {
                cart.Update(Convert.ToInt32(Request.QueryString["id"]),
                            Convert.ToInt32(Request.QueryString["qty"]));
            }
            return cart.NumberOfItems;
        }

        public int ItemsCount()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart != null)
            {
                return cart.NumberOfItems;
            }
            return 0;
        }

        public ActionResult Checkout()
        {
            User currentUser = (User)Session[WebUtil.CURRENT_USER];

            //User currentUser = new UserHandler().GetUser(user.Id);

            FinalOrder fo = new FinalOrder();

            if (currentUser != null)
            {
                fo.Email = currentUser.Email;
                fo.FullAddress = currentUser.FullAddress;
                fo.Name = currentUser.FullName;
                fo.Phone = currentUser.Phone;

                return RedirectToAction("ConfirmOrder", fo);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(FinalOrder order)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ConfirmOrder", order);
            }

            return View();
        }

        public ActionResult ConfirmOrder(FinalOrder order)
        {
            int total = 0;
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];

            if (cart != null && cart.NumberOfItems > 0)
            {

                foreach (var c in cart.Items)
                {
                    ShoppingCartItem cartItem = new ShoppingCartItem
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageURL = c.ImageURL,
                        Sale = c.Sale
                    };
                    order.ShoppingCartItem.Add(cartItem);
                    total += c.Amount;
                }
                ViewBag.total = total;

            }
            return View(order);
        }

        public ActionResult PlaceOrder(FinalOrder order)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];

            if (cart != null && cart.NumberOfItems > 0)
            {

                foreach (var c in cart.Items)
                {
                    ShoppingCartItem cartItem = new ShoppingCartItem
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageURL = c.ImageURL,
                        Sale = c.Sale
                    };
                    order.ShoppingCartItem.Add(cartItem);
                }

                order.OrderStatus = "Pending";
                dynamic randomNumber = Path.GetRandomFileName().Replace(".", "");
                order.OrderNumber = randomNumber;
            }
            new OrderHandler().AddOrder(order);
            Session.Clear();

            // Sending Product Email

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(order.Email));
                message.Subject = "-No-Reply- Shopping Details";

                // BODY Making Here to Send HTML page in email.

                message.IsBodyHtml = true;

                string body = string.Empty;
                StreamReader reader = new StreamReader(Server.MapPath("~/Views/Cart/orderemail.html"));
                using (reader)
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{track}", order.OrderNumber);
                body = body.Replace("{username}", order.Name);
                
                message.Body = body;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                    ViewBag.success = "Email Has been sent to  " + order.Email;
                }
                return View(order);

            }
            catch (Exception)
            {
                ViewBag.error = "Error Sending Mail. Please Try Again Later!";
            }
            return View(order);
        }

        public int OrderCount()
        {
            DBContextClass db = new DBContextClass();
            using (db)
            {
                return (from c in db.FinalOrders select c).Count();
            }
        }
    }
}
