using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Orders;
using ECommerce.Products;
using ECommerce.Exceptions;

namespace ECommerce.Data
{
    internal class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
        private static ProductRepository _instance;

        private ProductRepository() { }

        public static ProductRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductRepository();
                }

                return _instance;
            }
        }

        public void Add(Product item)
        {
            if (item != null)
            {
                _products.Add(item);
            }
            else
            {
                Console.WriteLine("Invalid item entry!");
            }
        }

        public void Remove(Product item)
        {
            _products.Remove(item);
        }

        public void Remove(int id)
        {
            foreach (Product item in _products)
            {
                if(item.ProductID == id)
                {
                    _products.Remove(item);
                    return;
                }
            }

            Console.WriteLine("Invalid Product ID!");
        }

        public Product Find(int id)
        {
            var product = _products.FirstOrDefault(item => item.ProductID == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                Console.WriteLine("Product not found!");
                return null;
            }
        }

        public static void GetAll()
        {
            _products.ForEach(item =>
            {
                item.DisplayProductInfo();
            });
        }
    }

    class OrderRepository
    {
        private static OrderRepository _instance;
        private Dictionary<int, Order> _orders = new Dictionary<int, Order>();

        private OrderRepository() { }

        public static OrderRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OrderRepository();
                }

                return _instance;
            }
        }

        public void Add(int id, Order item)
        {
            if (item != null)
            {
                _orders.Add(id, item);
            }
            else
            {
                Console.WriteLine("Invalid item entry!");
            }
        }

        public void Remove(int id)
        {
            _orders.Remove(id);
        }

        public Order Find(int id)
        {
            if (_orders.TryGetValue(id, out var order))
            {
                return order;
            }
            else
            {
                throw new OrderNotFoundException(id);
            }
        }

        public void GetAll()
        {
            _orders.Values.ToList().ForEach(item =>
            {
                item.DisplayOrderInfo();
            });
        }
    }
}