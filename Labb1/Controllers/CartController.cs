using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Labb1.ProjectData;
using Labb1.Services;
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
        private readonly ApiHandler _api;

        public CartController(UserManager<ApplicationUser> userManager, ApiHandler api)
        {
            _userManager = userManager;
            _api = api;
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