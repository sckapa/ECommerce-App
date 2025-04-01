using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Data;
using ECommerce.Products;
using ECommerce.Orders;
using ECommerce.Exceptions;
using ECommerce.Logging;

namespace ECommerce.Services
{
    class ProductService
    {
        private Product _product;
        private ProductRepository _productRepository = ProductRepository.Instance;

        public ProductService(Product product)
        {
            _product = product;
            if (_product.Quantity == 0)
            {
                _product.Quantity = 1;
            }
        }

        public void NewProduct()
        {
            _productRepository.Add(_product);
        }

        public void RemoveProduct()
        {
            _productRepository.Remove(_product);
        }

        public void AddAmountToInventory(int amount)
        {
            _product.Quantity += amount;
        }

        public void RemoveAmountFromInventory(int amount)
        {
            _product.Quantity -= amount;
        }

        public void SetAmount(int amount)
        {
            _product.Quantity = amount;
        }
    }

    class OrderService
    {
        private Order _order;
        private OrderRepository _orderRepository = OrderRepository.Instance;

        public delegate void OrderProcessedEventHandler(int id, OrderStatus status);
        public event OrderProcessedEventHandler OrderProcessed;

        public OrderService(Order order)
        {
            _order = order;
        }

        public async Task PlaceOrderAsync()
        {
            //await Task.Delay(3000);

            _order.OrderStatus = OrderStatus.Pending;
            Random random = new Random();
            int orderId = random.Next(1, int.MaxValue);

            await Task.Run(() =>
            {
                _order.OrderID = orderId;
                _order.OrderStatus = OrderStatus.Processed;
                _orderRepository.Add(orderId, _order);
            });

            OnOrderProcessed(orderId, OrderStatus.Processed);
        }

        public void CancelOrder()
        {
            _orderRepository.Remove(_order.OrderID);
            _order.OrderStatus = OrderStatus.Cancelled;

            OnOrderProcessed(_order.OrderID, OrderStatus.Cancelled);
        }

        public static async Task<OrderStatus> GetOrderStatusAsync(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return OrderRepository.Instance.Find(id).OrderStatus;
                }
                catch (OrderNotFoundException ex)
                {
                    Logger.LogError(ex.Message, ex);

                    Console.WriteLine($"Error: {ex.Message}");
                    return OrderStatus.Invalid;
                }
            });
        }

        private void OnOrderProcessed(int id, OrderStatus status)
        {
            if (OrderProcessed != null)
            {
                OrderProcessed.Invoke(id, status);
            }
        }

    }
}
