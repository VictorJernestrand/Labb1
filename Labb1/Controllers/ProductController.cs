using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Models;
using Labb1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web;
using Labb1.Helpers;
using Labb1.ProjectData;
using Labb1.Services;
using Microsoft.AspNetCore.Hosting;

namespace Labb1.Controllers
{
    public class ProductController : Controller
    {
        //private readonly Dummy _dummyData;
        public const string CartSessionKey = "CartId";
        private readonly ApiHandler _api;
        private readonly IWebHostEnvironment _env;
        public ProductController(ApiHandler api, IWebHostEnvironment env)
        {
            //_dummyData = new Dummy();
            _api = api;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            //var allProducts = _dummyData.Products;
            var allProducts = await _api.GetAllAsync<Product>(ApiHandler.PRODUCTS);
            return View(allProducts);
        }

        public async Task<IActionResult> SearchedProducts(string searchString)
        {
            var result = await _api.GetAllAsync<Product>(ApiHandler.PRODUCTS);
            //var result = from m in _api.GetAllAsync<Product>(ApiHandler.PRODUCTS).Result
            //             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                TempData["ResultCountMessage"] = $"Din sökning gav {result.Count()} träffar.";
            }
            
            return View(result);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            //var product = _dummyData.Products.FirstOrDefault(x => x.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            var product = await _api.GetOneAsync<Product>(ApiHandler.PRODUCTS + id);

            return View(product);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _api.GetOneAsync<Product>(ApiHandler.PRODUCTS + id);
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>
                {   
                //new CartItem { Product = _dummyData.Products.Find(x => x.Id == id), Quantity = 1 }
                new CartItem {Product = product, Quantity = 1 }
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                int index = cart.FindIndex(f => f.Product.Id == id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
    }
}