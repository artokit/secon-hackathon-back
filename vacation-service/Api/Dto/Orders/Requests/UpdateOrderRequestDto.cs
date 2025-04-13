using Common;
using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Orders.Requests
{
    public class UpdateOrderRequestDto
    {
        [Required(ErrorMessage = "Укажите какой приказ вы хотите поменять")]
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public Guid DepartamentId { get; set; }
        public DateTime CreatedData { get; set; }
        public OrderStatus Status { get; set; }
    }
}
