using DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.Dto.Requests.Requests
{
    public class UpdateRequestRequestDto
    {
        public Guid Id { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}
