using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Labb1.Services;
using Labb1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Labb1.Controllers
{

    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApiHandler _api;
        public HttpClient _client { get; set; }

        public OrderController(UserManager<ApplicationUser> userManager, ApiHandler api, IHttpClientFactory client)
        {
            _userManager = userManager;
            _api = api;
            _client = client.CreateClient();

        }
        //[HttpPost]
        //public IActionResult Index(OrderViewModel orderViewModel)
        //{
        //    orderViewModel = TempData["Order"] as OrderViewModel;
        //    return View(orderViewModel);
        //}
        [HttpPost]
        public async Task<IActionResult> Index([Bind("TotalPrice, CartItems")]CartViewModel cartViewModel)
        {
            // Error handling. 
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                // get the cart from session
                var cart = await GetCart();

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
                    // Add products in order for each product in cart
                    var orderProducts = cart.Select(x => new OrderProduct
                    {
                        Product = x.Product,
                        ProductId = x.Product.Id,
                        Quantity = x.Quantity
                    }).ToList();

                    // Remove all products in cart where quantity is 0
                    var itemsToRemove = from items in orderProducts
                                        where items.Quantity == 0
                                        select items;
                    foreach (var item in itemsToRemove.ToList())
                    {
                        orderProducts.Remove(item);
                    }
                    // Prevent user to confirm order without products in cart
                    if (orderProducts.Count == 0)
                    {
                        TempData["MessageCartInfo"] = "Du kan inte lägga en order, din kundkorg är tom.";
                        return RedirectToAction("Index");
                    }


                    OrderViewModel orderViewModel = new OrderViewModel();
                    Order order = new Order
                    {
                        OrderDate = DateTime.Now,
                        //UserId = Guid.Parse(_userManager.GetUserId(User)),
                        User = user,
                        UserId = user.Id,
                        OrderProducts = orderProducts,
                        TotalPrice = cartViewModel.TotalPrice
                    };


                    orderViewModel.Order = order;
                    orderViewModel.User = user;
                    orderViewModel.Order.OrderProducts = orderProducts;
                    orderViewModel.Order.TotalPrice = cartViewModel.TotalPrice;




                    //HTTP POST
                    //var postTask = client.PostAsJsonAsync<OrderViewModel>("orders", orderViewModel);
                    var postTask = _client.PostAsJsonAsync<Order>(ApiHandler.ORDERS, order);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("Index");
                    }

                    //using (var client = new HttpClient())
                    //{
                    //    client.BaseAddress = new Uri("http://localhost:44323/api/orders/");

                    //    //HTTP POST
                    //    var postTask = client.PostAsJsonAsync<Order>("order", order);
                    //    postTask.Wait();

                    //    var result = postTask.Result;
                    //    if (result.IsSuccessStatusCode)
                    //    {
                    //        return RedirectToAction("Index");
                    //    }
                    //}

                    //ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                    //return View(order);

                    //var test = await ApiHandler.PostAsync<OrderViewModel>(orderViewModel, ApiHandler.ORDERS);

                    // Clear cart session
                    HttpContext.Session.Remove("cart");

                    return View(orderViewModel);
                    //TempData["Order"] = orderViewModel;
                    //return RedirectToAction("Index", "Order");
                }
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<List<CartItem>> GetCart()
        {
            return await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        }
    }
}