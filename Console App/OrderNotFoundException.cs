
namespace ECommerce.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public int OrderId { get; }

        public OrderNotFoundException(int orderId)
            : base($"Order with ID {orderId} was not found.")
        {
            OrderId = orderId;
        }
        
        public OrderNotFoundException(int orderId, Exception innerException)
            : base($"Order with ID {orderId} was not found.", innerException)
        {
            OrderId = orderId;
        }
    }
}
