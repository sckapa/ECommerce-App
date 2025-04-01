using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Users;

namespace ECommerce.Orders
{
    public enum OrderStatus
    {
        Invalid = -1,
        Pending,
        Processed,
        Shipped,
        Delivered,
        Cancelled
    }

    struct Address
    {
        public string Street;
        public string City;
        public int ZipCode;
    }
}
