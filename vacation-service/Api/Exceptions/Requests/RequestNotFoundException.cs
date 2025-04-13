using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Requests
{
    public class RequestNotFoundException : NotFoundRequestException
    {
        public RequestNotFoundException(string? message = "Request не найден") : base(message) { }
    }
}
