using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Dto.Users.Responses;

namespace Api.Services.Interfaces;

public interface IDepartmentService
{
    public Task<GetDepartmentResponseDto> CreateAsync(Guid userId, CreateDepartmentRequestDto createDepartmentRequestDto);  
    public Task<GetDepartmentFullInfoResponseDto> GetByIdAsync(Guid userId, Guid id);
    // public Task<List<GetDepartmentResponseDto>> GetAllAsync(Guid userId);
    public Task<GetDepartmentResponseDto> UpdateAsync(Guid userId, Guid id, UpdateDepartmentRequestDto updateDepartmentRequestDto);
    public Task DeleteAsync(Guid userId, Guid id);
    public Task<List<GetDepartmentResponseDto>> GetChidrenDepartments(Guid departmentId);
    public Task<List<GetUserResponseDto>> GetDepartmentUsersAsync(Guid departmentId);
    public Task<List<GetDepartmentResponseDto>> GetUserDepartmentsAsync(Guid userId);
}