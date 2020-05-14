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

namespace Labb1.Controllers
{
    public class ProductController : Controller
    {
        private readonly Dummy _dummyData;
        public const string CartSessionKey = "CartId";
        public ProductController()
        {
            _dummyData = new Dummy();
        }
        public IActionResult Index()
        {
            var allProducts = _dummyData.Products;
            return View(allProducts);
        }

        public IActionResult ProductDetail(int id)
        {
            var product = _dummyData.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>
                {
                    new CartItem { Product = _dummyData.Products.Find(x => x.Id == id), Quantity = 1 }
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
                    cart.Add(new CartItem { Product = _dummyData.Products.Find(x => x.Id == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
    }
}