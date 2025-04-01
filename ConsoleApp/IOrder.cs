using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders
{
    interface IOrders
    {
        int OrderID { get; set; }
        Address address { get; set; }
        OrderStatus OrderStatus { get; set; }
        void DisplayOrderInfo();
    }

    class Order : IOrders
    {
        public int OrderID { get; set; }
        public Address address { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int OrderProduct { get; set; }
        public int OrderAmount { get; set; }

        public void DisplayOrderInfo()
        {
            Console.WriteLine("Order ID : " + OrderID);
            Console.WriteLine($"Order Address : {address.Street}, {address.City} - {address.ZipCode}");
            Console.WriteLine("Order Status : " + OrderStatus);
        }
    }
}
