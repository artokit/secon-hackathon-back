using System.ComponentModel.DataAnnotations;
using Common;
using DataAccess.Models;

namespace Api.Dto.Requests.Requests
{
    public class CreateRequestRequestDto
    {
        public List<RangeDto> Ranges { get; set; }
    }

    public class RangeDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
