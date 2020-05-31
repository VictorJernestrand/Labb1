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
                        return RedirectToAction("Index", "Cart");
                    }


                    OrderViewModel orderViewModel = new OrderViewModel();
                    Order order = new Order
                    {
                        OrderDate = DateTime.Now,
                        User = user,
                        UserId = user.Id,
                        OrderProducts = orderProducts,
                        TotalPrice = cartViewModel.TotalPrice
                    };

                    orderViewModel.Order = order;
                    orderViewModel.User = user;
                    orderViewModel.Order.OrderProducts = orderProducts;
                    orderViewModel.Order.TotalPrice = cartViewModel.TotalPrice;

                    var postTask = _client.PostAsJsonAsync<Order>(ApiHandler.POST, order);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        // Clear cart session
                        HttpContext.Session.Remove("cart");

                        return View(orderViewModel);
                    }
                    else
                    {
                        return NotFound();
                    }
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