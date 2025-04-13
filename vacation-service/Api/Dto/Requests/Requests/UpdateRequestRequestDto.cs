using DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Requests.Requests
{
    public class UpdateRequestRequestDto
    {
        [Required(ErrorMessage = "Укажите какой request вы хотите поменять")]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExtensionsDays { get; set; }
        public DateTime FactDate { get; set; }
        public Guid ReasonId { get; set; }
        public string Comment { get; set; }
    }
}
