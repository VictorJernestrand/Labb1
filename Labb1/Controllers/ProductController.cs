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
        private string _cartSessionCookie;
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public ProductController(IConfiguration config)
        {
            this._cartSessionCookie = config["CartSessionCookie:Name"];
        }
        public IActionResult Index()
        {
            Dummy dummyData = new Dummy();
            //var test = dummyData.Products.Where(x => x.Name == "Ahlgrens bilar");

            var allProducts = dummyData.Products;

            //ProductViewModel allProducts = new ProductViewModel();

            return View(allProducts);
        }
        //public IActionResult AddToCart(Guid id)
        //{

        //Guid guid = Guid.NewGuid();

        //if (HttpContext.Session.GetString(_cartSessionCookie) == null)
        //{
        //    HttpContext.Session.SetString(_cartSessionCookie, guid.ToString());
        //}



        //Cart cart = new Cart()
        //{
        //    Id = Guid.Parse(HttpContext.Session.GetString(_cartSessionCookie)),
        //    ProductId = id,
        //    Amount = 1
        //};

        //var cartContent = 

        //---------------------------------------

        //var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
        //string cartContent = "";

        //if (cart.Value != null)
        //{
        //    cartContent = cart.Value;
        //    cartContent += ("," + id);
        //}
        //else
        //{
        //    cartContent += id;
        //}

        //Response.Cookies.Append("cart", cartContent);

        //return RedirectToAction("Index");


        //-------------------------------------------------

        //ShoppingCartId = Get

        //}

        //public string GetCartId()
        //{
        //    //if (HttpContext.Current.Session[CartSessionKey] == null)
        //    if (HttpContext.Session.GetString(CartSessionKey) == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
        //        {

        //        }
        //    }
        //}
        public IActionResult AddToCart(int id)
        {

            //Product product = new Product();
            Dummy dummyData = new Dummy();

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = dummyData.Products.Find(x => x.Id == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                int index = cart.FindIndex(f => f.Product.Id == id);
                //if (index >= 0)
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = dummyData.Products.Find(x => x.Id == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }


    }
}