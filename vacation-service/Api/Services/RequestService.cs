using Api.Dto.Requests.Requests;
using Api.Dto.Requests.Responses;
using Api.Exceptions.Requests;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        public RequestService(IRequestRepository requestRepository) 
        { 
            _requestRepository = requestRepository;
        }

        public async Task<GetRequestResponse> CreateAsync(CreateRequestRequestDto createRequestRequestDto)
        {
            var dbRequest = createRequestRequestDto.MapToDb();

            var createdRequest = await _requestRepository.CreateRequestAsync(dbRequest);
            return createdRequest.MapToDto();
        }

        public async Task<GetRequestResponse> GetByIdAsync(Guid id)
        {
            var request = await _requestRepository.GetRequestByIdAsync(id);

            if (request is null)
            {
                throw new RequestNotFoundException(); 
            }

            return request.MapToDto();
        }

        public async Task<GetRequestResponse> UpdateAsync(Guid id, UpdateRequestRequestDto updateRequestRequestDto)
        {
            if (await _requestRepository.GetRequestByIdAsync(id) is null)
            {
                throw new RequestNotFoundException();
            }

            var dbRequest = updateRequestRequestDto.MapToDb();
            var updatedRequest = await _requestRepository.UpdateRequestAsync(id, dbRequest);

            return updatedRequest.MapToDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _requestRepository.GetRequestByIdAsync(id) is null)
            {
                throw new RequestNotFoundException();
            }

            await _requestRepository.DeleteRequestAsync(id);
        }
    }
}
