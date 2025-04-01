using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Data;
using ECommerce.Exceptions;
using ECommerce.Orders;
using ECommerce.Products;
using ECommerce.Services;
using ECommerce.Users;

namespace Prac
{
    internal class MainClass
    {
        public static void Main()
        {
            User user;
            string? input = "";

            do
            {
                Console.WriteLine("Welcome! Please choose user type : ");
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit");
                Console.Write("Input : ");
                input = Console.ReadLine();
                Console.WriteLine("");

                if (input == "1" || input == "customer" || input == "Customer")
                {
                    do
                    {
                        Console.WriteLine("Please choose action : ");
                        Console.WriteLine("1. Login/Register");
                        Console.WriteLine("2. Browse Products");
                        Console.WriteLine("3. Place Order");
                        Console.WriteLine("4. View Order Status");
                        Console.WriteLine("5. Exit");
                        Console.Write("Input : ");
                        input = Console.ReadLine();
                        Console.WriteLine("");

                        if (input == "1")
                        {
                            Console.Write("Please enter Name : ");
                            string? name = Console.ReadLine();
                            while (name == null)
                            {
                                Console.Write("Please enter a valid Name : ");
                                name = Console.ReadLine();
                            }

                            Console.Write("Please enter Password : ");
                            string? pass = Console.ReadLine();
                            while (pass == null)
                            {
                                Console.Write("Please enter a valid Password : ");
                                pass = Console.ReadLine();
                            }

                            user = UserFactory.CreateUser("customer", name, pass);
                            Console.WriteLine("");
                        }
                        else if (input == "2")
                        {
                            ProductRepository.GetAll();
                            Console.WriteLine("");
                        }
                        else if (input == "3")
                        {
                            Order order = new Order();
                            Address address = new Address();

                            Console.Write("Please enter the ID of the product : ");
                            input = Console.ReadLine();
                            order.OrderProduct = Convert.ToInt32(input);

                            Console.Write("Please enter the amount to order : ");
                            input = Console.ReadLine();
                            order.OrderAmount = Convert.ToInt32(input);

                            Console.WriteLine("Please enter Address");
                            Console.Write("Street : ");
                            address.Street = Console.ReadLine();

                            Console.Write("City : ");
                            address.City = Console.ReadLine();

                            Console.Write("Zip Code : ");
                            address.ZipCode = Convert.ToInt32(Console.ReadLine());

                            order.address = address;
                            OrderService orderService = new OrderService(order);

                            Task.Run(() => orderService.PlaceOrderAsync());
                            Console.WriteLine("");
                        }
                        else if (input == "4")
                        {
                            Console.Write("Please enter Order ID : ");
                            input = Console.ReadLine();

                            Task.Run(() => OrderService.GetOrderStatusAsync(Convert.ToInt32(input))).Wait();

                            Console.WriteLine("");
                        }

                    }
                    while (Convert.ToInt32(input) != 5);
                }
                else if (input == "2" || input == "admin" || input == "Admin")
                {
                    do
                    {
                        Console.WriteLine("Please choose action : ");
                        Console.WriteLine("1. Manage Products");
                        Console.WriteLine("2. View All Orders");
                        Console.WriteLine("3. Exit");
                        Console.Write("Input : ");
                        input = Console.ReadLine();
                        Console.WriteLine("");

                        if (Convert.ToInt32(input) == 1)
                        {
                            do
                            {
                                Console.WriteLine("1. View All");
                                Console.WriteLine("2. New Product");
                                Console.WriteLine("3. Remove Product");
                                Console.WriteLine("4. Change Product Quantity");
                                Console.WriteLine("5. Exit");
                                Console.Write("Input : ");
                                input = Console.ReadLine();
                                Console.WriteLine("");

                                if (Convert.ToInt32(input) == 1)
                                {
                                    ProductRepository.GetAll();
                                    Console.WriteLine("");
                                }
                                else if (Convert.ToInt32(input) == 2)
                                {
                                    Product product = new Product();

                                    Console.Write("Enter Product name : ");
                                    input = Console.ReadLine();
                                    product.Name = input;

                                    Console.Write("Enter Product price : ");
                                    input = Console.ReadLine();
                                    product.Price = Convert.ToInt32(input);

                                    Console.Write("Enter Product quantity : ");
                                    input = Console.ReadLine();
                                    product.Quantity = Convert.ToInt32(input);

                                    Random random = new Random();
                                    product.ProductID = random.Next();

                                    ProductService productService = new ProductService(product);
                                    productService.NewProduct();

                                    Console.WriteLine("");
                                }
                                else if (Convert.ToInt32(input) == 3)
                                {
                                    Console.Write("Enter Product ID : ");
                                    input = Console.ReadLine();

                                    ProductRepository productRepository = ProductRepository.Instance;
                                    productRepository.Remove(Convert.ToInt32(input));

                                    Console.WriteLine("");
                                }
                                else if (Convert.ToInt32(input) == 4)
                                {
                                    do
                                    {
                                        Console.WriteLine("1. Add");
                                        Console.WriteLine("2. Remove");
                                        Console.WriteLine("3. Set");
                                        Console.WriteLine("4. Exit");
                                        input = Console.ReadLine();

                                        if (Convert.ToInt32(input) == 1)
                                        {
                                            Console.Write("Enter Product ID : ");
                                            string? id = Console.ReadLine();

                                            Console.WriteLine("Enter amount to be added : ");
                                            string? amount = Console.ReadLine();

                                            ProductRepository productRepository = ProductRepository.Instance;
                                            var pro = productRepository.Find(Convert.ToInt32(id));

                                            ProductService productService = new ProductService(pro);
                                            productService.AddAmountToInventory(Convert.ToInt32(amount));
                                        }
                                        else if (Convert.ToInt32(input) == 2)
                                        {
                                            Console.Write("Enter Product ID : ");
                                            string? id = Console.ReadLine();

                                            Console.WriteLine("Enter amount to be removed : ");
                                            string? amount = Console.ReadLine();

                                            ProductRepository productRepository = ProductRepository.Instance;
                                            var pro = productRepository.Find(Convert.ToInt32(id));

                                            ProductService productService = new ProductService(pro);
                                            productService.RemoveAmountFromInventory(Convert.ToInt32(amount));
                                        }
                                        else if (Convert.ToInt32(input) == 3)
                                        {
                                            Console.Write("Enter Product ID : ");
                                            string? id = Console.ReadLine();

                                            Console.WriteLine("Enter amount : ");
                                            string? amount = Console.ReadLine();

                                            ProductRepository productRepository = ProductRepository.Instance;
                                            var pro = productRepository.Find(Convert.ToInt32(id));

                                            ProductService productService = new ProductService(pro);
                                            productService.SetAmount(Convert.ToInt32(amount));
                                        }
                                    }
                                    while (Convert.ToInt32(input) != 4);
                                }
                            }
                            while (Convert.ToInt32(input) != 5);
                        }
                        else if (Convert.ToInt32(input) == 2)
                        {
                            OrderRepository orderRepository = OrderRepository.Instance;
                            orderRepository.GetAll();
                            Console.WriteLine("");
                        }
                    }
                    while (Convert.ToInt32(input) != 3);
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
            while (Convert.ToInt32(input) != 3);
    }
    }
}