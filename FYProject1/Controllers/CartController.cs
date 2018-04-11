
using FYProject1Classes;
using FYProject1Classes.CartManagment;
using System;
using System.Collections.Generic;
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

            }
            new OrderHandler().AddOrder(order);
            Session.Clear();
            return View(order);
        }
    }
}