using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Labb1.ProjectData;
using Labb1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    public class CartController : Controller
    {
        private string _cartCookie;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(/*IConfiguration config*/UserManager<ApplicationUser> userManager)
        {
            //this._cartCookie = config["CartSessionCookie:Name"];
            _userManager = userManager;
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

            //-----------------------------------------------------------

            //if (HttpContext.Session.GetString(_cartCookie) != null)
            //{
            //    var customerCartId = HttpContext.Session.GetString(_cartCookie);
            //    Guid cartId = Guid.Parse(customerCartId);
            //    Dummy dummyData = new Dummy();

            //    return View(result);
            //}

            //return View();



            //------------------------------------------------------

            

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                //ViewBag.cart = cart;

                //ViewBag.total = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);

                CartViewModel cartViewModel = new CartViewModel();
                cartViewModel.CartItems = cart;
                cartViewModel.TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);

                return View(cartViewModel);
            }

            else
            {
                return View();
            }

            //if (total == 0)
            //{
            //    return View();
            //}

            //else
            //{
            //    return Redirect(Request.Path.ToString());
            //}

            //}
            //    var test = cart.Select(x => new CartItem
            //{
            //    Product = x.Product,
            //    Quantity = x.Quantity
            //}).ToList();
            
            //else
            //{
            //   return RedirectToAction("Index", "Product");
            //}
            
        }

        private static decimal TotalCost(int quantity, decimal price)
        {
            return price * quantity;
        }
    }
}