using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    public class CartController : Controller
    {
        private string _cartCookie;
        public CartController(IConfiguration config)
        {
            this._cartCookie = config["CartSessionCookie:Name"];
        }
        [Route("cart")]
        public IActionResult Index()
        {
            //HttpCookie cart = Request.Cookies["cart"];


            //------------------------


            //var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");

            //if (cart.Value != null)
            //{
            //    var cartId = HttpContext.Request
            //}
            //return View();


            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);
            return View();
        }
    }
}