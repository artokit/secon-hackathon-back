using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.Dto.Orders.Requests
{
    public class CreateOrderRequestDto
    {
        [Required]
        public Guid CreatorId { get; set; }
        [Required]
        public Guid DepartamentId { get; set; }
        [Required]
        public DateTime CreatedData { get; set; }
        [Required]
        public OrderStatus Status { get; set; }

    }
}
