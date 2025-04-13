using Common;

namespace Api.Dto.Orders.Responses
{
    public class GetOrderResponseDto
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public Guid DepartamentId { get; set; }
        public DateTime CreatedData { get; set; }
        public OrderStatus Status { get; set; }
    }
}