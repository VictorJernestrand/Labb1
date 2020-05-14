using System;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        // Authorize so only logged in users can access shopping cart
        [Authorize]
        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                return View(await ShowCart());
            }
            else
            {
                return View();
            }        
        }
        public async Task<IActionResult> IncreaseInCart(int id)
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = await GetCart();
                int index = FindIndexOfCartItem(cart, id);
                if (index != -1)
                {
                    // Increase one copy from that specific product
                    cart[index].Quantity++;
                }
                SetCart(cart);

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> ReduceFromCart(int id)
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = await GetCart();
                int index = FindIndexOfCartItem(cart, id);
                if (index != -1)
                {
                    // Reduce one copy from that specific product
                    cart[index].Quantity--;
                }
                SetCart(cart);

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();               
            }
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                var cart = await GetCart();
                int index = FindIndexOfCartItem(cart, id);
                if (index != -1)
                {
                    // Remove all of this product from cart
                    cart.RemoveAt(index);
                }
                SetCart(cart);

                return RedirectToAction("Index");
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
                        UserId = Guid.Parse(_userManager.GetUserId(User))
                    };


                orderViewModel.Order = order;
                orderViewModel.User = user;
                orderViewModel.Order.OrderProducts = orderProducts;
                orderViewModel.Order.TotalPrice = cartViewModel.TotalPrice;

                  
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
        public async Task<CartViewModel> ShowCart()
        {
            var cart = await GetCart();
            CartViewModel cartViewModel = new CartViewModel
            {
                CartItems = cart,
                TotalPrice = cart.Sum(cartItem => cartItem.Product.Price * cartItem.Quantity)
            };
            return cartViewModel;
        }
        public async Task<List<CartItem>> GetCart()
        {
            return await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        }
        public void SetCart(List<CartItem> cart)
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }
        public int FindIndexOfCartItem(List<CartItem> cart, int id)
        {
            return cart.FindIndex(f => f.Product.Id == id);
        }
    }
}