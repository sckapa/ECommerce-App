using ECommerce.Orders;

namespace ECommerce.Users
{
    public abstract class User
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string Role { get; protected set; }

        protected User(int userId, string userName, string role)
        {
            UserId = userId;
            UserName = userName;
            Role = role;
        }

        public abstract void DisplayUserInfo();
    }

    public class Customer : User
    {
        public string CustomerPassword { get; private set; }

        public Customer(int userId, string name, string password)
            : base(userId, name, "Customer")
        {
            CustomerPassword = password;
        }

        public override void DisplayUserInfo()
        {
            Console.WriteLine($"Customer ID: {UserId}");
            Console.WriteLine($"Customer Name: {UserName}");
            Console.WriteLine($"Role: {Role}");
        }

        public void OrderProcessedHandler(int id, OrderStatus status)
        {
            Console.WriteLine($"Order Status - ID : {id}, Status : {status}");
        }
    }

    public class Admin : User
    {
        public string AdminPassword { get; private set; }

        public Admin(int userId, string name, string password)
            : base(userId, name, "Admin")
        {
            AdminPassword = password;
        }

        public override void DisplayUserInfo()
        {
            Console.WriteLine($"Admin ID: {UserId}");
            Console.WriteLine($"Admin Name: {UserName}");
            Console.WriteLine($"Role: {Role}");
        }
    }

    public static class UserFactory
    {
        private static int _idCounter = 1; 

        public static User CreateUser(string userType, string name, string password)
        {
            int userId = _idCounter++; 

            return userType.ToLower() switch
            {
                "customer" => new Customer(userId, name, password),
                "admin" => new Admin(userId, name, password),
                _ => throw new ArgumentException("Invalid user type")
            };
        }
    }
}
