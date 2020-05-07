﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Labb1.ProjectData;
using Labb1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Labb1.Controllers
{
    public class CartController : Controller
    {
        //private string _cartCookie;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly HttpContextAccessor _httpContextAccessor;

        public CartController(/*IConfiguration config*/UserManager<ApplicationUser> userManager /*HttpContextAccessor httpContextAccessor*/)
        {
            //this._cartCookie = config["CartSessionCookie:Name"];
            _userManager = userManager;
            //_httpContextAccessor = httpContextAccessor;

        }
        // Authorize so only logged in users can access shopping cart
        [Authorize]
        [Route("cart")]
        public IActionResult Index()
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                //ViewBag.cart = cart;

                //ViewBag.total = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);

                CartViewModel cartViewModel = new CartViewModel();
                //cartViewModel.Id = Guid.NewGuid();
                cartViewModel.CartItems = cart;
                cartViewModel.TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);

                return View(cartViewModel);
            }

            else
            {
                return View();
            }        
        }
        public IActionResult AddToCart(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                CartViewModel cartViewModel = new CartViewModel();
                cartViewModel.CartItems = cart;
                cartViewModel.TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);




                int index = cart.FindIndex(f => f.Product.Id == id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return View("Index", cartViewModel);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult ReduceFromCart(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                CartViewModel cartViewModel = new CartViewModel();
                cartViewModel.CartItems = cart;
                cartViewModel.TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);


                int index = cart.FindIndex(f => f.Product.Id == id);
                if (index != -1)
                {
                    cart[index].Quantity--;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return View("Index", cartViewModel);
            }

            else
            {

                return NotFound();
                
            }
        }

        public IActionResult RemoveFromCart(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                CartViewModel cartViewModel = new CartViewModel();
                cartViewModel.CartItems = cart;
                cartViewModel.TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity);


                int index = cart.FindIndex(f => f.Product.Id == id);
                if (index != -1)
                {
                    cart.RemoveAt(index);
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return View("Index", cartViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder([Bind("TotalPrice, CartItems")]CartViewModel cartViewModel)
        {

            // Error handling. 
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                // get the cart from session
                var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                //OrderViewModel ovm = new OrderViewModel();
                //ovm.Order.OrderProducts = cart;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.FirstName) ||
                    String.IsNullOrEmpty(user.StreetAddress) || String.IsNullOrEmpty(user.ZipCode) ||
                    String.IsNullOrEmpty(user.City))
                {
                    TempData["MessageAddressInfo"] = "Vänligen fyll i ditt namn och adress innan du kan beställa";
                    return LocalRedirect("/Identity/Account/Manage");
                }
                else
                { 

                // add products in order for each product in cart
                var orderProducts = cart.Select(x => new OrderProduct
                {
                    Product = x.Product,
                    Quantity = x.Quantity
                }).ToList();


                // Get the user
                //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var user = await _userManager.GetUserAsync(HttpContext.User);

                // Create new order


                //var orderProds = from item in cartViewModel.CartItems
                //                 select new OrderProduct
                //                 {
                //                     Product = item.Product,
                //                     Quantity = item.Quantity
                //                 };

                //List<OrderProduct> prods = orderProds.ToList();


                //List<OrderProduct> productList = new List<OrderProduct>();


                OrderViewModel orderViewModel = new OrderViewModel();

                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.UserId = Guid.Parse(_userManager.GetUserId(User));


                //Order order = new Order()
                //{
                //    UserId = Guid.Parse(_userManager.GetUserId(User)),
                //    OrderProducts = orderProducts,
                //    TotalPrice = cartViewModel.TotalPrice
                //};
                //orderViewModel.Order = order;
                //orderViewModel.User = user;




                //foreach (var item in cartViewModel.CartItems)
                //{
                //    order.OrderProducts.Add((OrderProduct)item);
                //}
                orderViewModel.Order = order;
                orderViewModel.User = user;
                orderViewModel.Order.OrderProducts = orderProducts;
                orderViewModel.Order.TotalPrice = cartViewModel.TotalPrice;



                //OrderViewModel orderViewModel = new OrderViewModel()
                //{
                //    Order = order,
                //    //Order = order,
                //    User = user
                //};

                // Clear cart session
                HttpContext.Session.Remove("cart");

                return View("OrderConfirmed", orderViewModel);
                }
            }
            else
            {
                return NotFound();
            }
        }
        //[HttpGet]
        //public IActionResult OrderConfirmed(OrderViewModel orderViewModel)
        //{

        //    return View(orderViewModel);
        //}

        private static decimal TotalCost(int quantity, decimal price)
        {
            return price * quantity;
        }
    }
}