using Api.Dto.Departments.Responses;
using Api.Dto.Positions.Requests;
using Api.Dto.Positions.Responses;
using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface IPositionService
{
    public Task<List<GetPositionResponseDto>> GetPositionsByDepartmentIdAsync(Guid id);
    public Task<GetPositionResponseDto> AddPositionAsync(CreatePositionRequestDto positionRequestDto);
    public Task<GetPositionResponseDto> UpdatePositionAsync(UpdatePositionRequestDto requestDto);
    public Task DeletePositionAsync(Guid id);
}