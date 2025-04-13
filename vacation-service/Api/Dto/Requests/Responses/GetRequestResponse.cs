using DataAccess.Models;

namespace Api.Dto.Requests.Responses
{
    public class GetRequestResponse
    {
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
