using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1.Helpers;
using Labb1.Models;
using Labb1.ProjectData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly Dummy _dummyData;
        public CartController()
        {
            _dummyData = new Dummy();
        }
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = _dummyData.Products.Find(x => x.Id == id), Quantity = 1 });
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
            return Ok();

        }
        //public async Task<IActionResult> IncreaseInCart(int id)
        //{
        //    if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
        //    {
        //        var cart = await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        //        int index = cart.FindIndex(f => f.Product.Id == id);
        //        if (index != -1)
        //        {
        //            cart[index].Quantity++;
        //        }
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        //public async Task<IActionResult> ReduceFromCart(int id)
        //{
        //    if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
        //    {
        //        var cart = await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        //        int index = cart.FindIndex(f => f.Product.Id == id);
        //        if (index != -1)
        //        {
        //            cart[index].Quantity--;
        //        }
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //public async Task<IActionResult> RemoveFromCart(int id)
        //{
        //    if (await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
        //    {
        //        var cart = await SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        //        int index = cart.FindIndex(f => f.Product.Id == id);
        //        if (index != -1)
        //        {
        //            cart.RemoveAt(index);
        //        }
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}