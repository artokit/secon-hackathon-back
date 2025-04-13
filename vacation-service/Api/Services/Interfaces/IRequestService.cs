using Api.Dto.Requests.Requests;
using Api.Dto.Requests.Responses;

namespace Api.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<GetRequestResponse> CreateAsync(CreateRequestRequestDto createRequestRequestDto);
        public Task<GetRequestResponse> GetByIdAsync(Guid id);
        public Task<GetRequestResponse> UpdateAsync(Guid id, UpdateRequestRequestDto updateRequestRequest);
        public Task DeleteAsync(Guid id);
    }
}
