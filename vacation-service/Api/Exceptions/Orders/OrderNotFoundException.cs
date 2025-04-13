using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Orders
{
    public class OrderNotFoundException : NotFoundRequestException
    {
        public OrderNotFoundException(string? message = "Приказ не найден") : base(message) { }
    }
}
