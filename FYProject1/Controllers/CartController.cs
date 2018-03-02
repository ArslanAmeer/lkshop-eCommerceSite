
using FYProject1Classes;
using FYProject1Classes.CartManagment;
using FYProject1Classes.ProductMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYProject1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public ActionResult ViewCart()
        {
            // Passing all Brands from Database to MAIN-LAYOUT
            List<Brand> brands = new List<Brand>();
            brands = new ProductHandler().GetBrands();
            ViewBag.brands = brands;

            // Passing Category List from Database to MAIN-LAYOUT

            List<Category> categories = new List<Category>();
            categories = new ProductHandler().GetCategories();
            ViewBag.categories = categories;

            int total = 0;
            List<ShoppingCartItem> cartList = null;
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];

            if (cart != null && cart.NumberOfItems > 0)
            {
                cartList = new List<ShoppingCartItem>();

                foreach (var c in cart.Items)
                {
                    ShoppingCartItem cartItem = new ShoppingCartItem
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageURL = c.ImageURL
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
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart == null)
            {
                cart = new ShoppingCart();
            }

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
            else
            {
                return 0;
            }
        }
    }
}