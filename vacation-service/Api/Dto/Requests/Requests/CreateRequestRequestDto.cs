using System.ComponentModel.DataAnnotations;
using Common;
using DataAccess.Models;

namespace Api.Dto.Requests.Requests
{
    public class CreateRequestRequestDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int ExtensionsDays { get; set; }
        [Required]
        public DateTime FactDate { get; set; }
        [Required]
        public Guid ReasonId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
