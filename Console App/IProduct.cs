using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Orders;

namespace ECommerce.Products
{
    internal interface IProduct
    {
        int ProductID { get; }
        string Name { get; }
        int Price { get; }
        int Quantity { get; set; }

        void DisplayProductInfo();
    }

    class Product : IProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public void DisplayProductInfo() 
        {
            Console.WriteLine("Product ID : " + ProductID);
            Console.WriteLine("Product Name : " + Name);
            Console.WriteLine("Product Price : " + Price);
            Console.WriteLine("Available Product Quantity : " + Quantity);
        }
    }
}
