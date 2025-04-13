namespace Api.Exceptions.Orders
{
    public class OrderArgumentException : ArgumentException
    {
        public OrderArgumentException(string? message = "Order ID cannot be null.") : base(message) { }
    }
}
