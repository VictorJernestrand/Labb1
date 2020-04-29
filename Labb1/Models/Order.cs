using Labb1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDate = DateTime.Now;
            OrderProducts = new List<OrderProduct>();
        }
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public decimal TotalPrice { get; set; }



    }

    //public class OrderProduct
    //{
    //    public OrderProduct()
    //    {

    //    }
    //    public OrderProduct(CartItem cartItem)
    //    {

    //        Product = cartItem.Product;
    //        Quantity = cartItem.Quantity;

    //    }
    //    //public Guid Id { get; set; }
    //    public int Quantity { get; set; }
    //    //public decimal Price { get; set; }
    //    public Product Product { get; set; }

    //    public static explicit operator OrderProduct(CartItem cartItem)
    //    {
    //        var orderProduct = new OrderProduct()
    //        {
    //            Product = cartItem.Product,
    //            Quantity = cartItem.Quantity
    //        };
    //        return orderProduct;
    //    }


    //}
}
