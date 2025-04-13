using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Dto.Users.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Application.Common.Interfaces;
using Application.FileService.Models;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;

namespace Api.Services;

public class DepartmentService : IDepartmentService
{
    private IDepartmentsRepository _departmentsRepository;
    private IUsersRepository _usersRepository;
    private IFileServiceClient _fileServiceClient;

    public DepartmentService(IDepartmentsRepository departmentsRepository, IUsersRepository usersRepository, IFileServiceClient fileServiceClient)
    {
        _departmentsRepository = departmentsRepository;
        _usersRepository = usersRepository;
        _fileServiceClient = fileServiceClient;
    }

    public async Task<GetDepartmentResponseDto> CreateAsync(Guid userId, CreateDepartmentRequestDto registerRequestDto)
    {
        if (registerRequestDto == null)
            throw new ArgumentNullException(nameof(registerRequestDto));

        if (userId == Guid.Empty)
            throw new UserArgumentException();

        var user = await _usersRepository.GetByIdAsync(userId);
        
        if (user == null)
            throw new UserNotFoundException();
        
        var parentDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);
        
        // Проверка что структуры нету.
        if (parentDepartment == null)
            throw new DepartmentNotFoundException();
        
        var dbDepartment = registerRequestDto.MapToDb();
        dbDepartment.ParentDepartmentId = parentDepartment.Id;
        dbDepartment.Role = registerRequestDto.Role;
        
        var res = await _departmentsRepository.CreateDepartmentAsync(dbDepartment);
        
        return res.MapToDto();
    }

    public async Task<GetDepartmentFullInfoResponseDto> GetByIdAsync(Guid userId, Guid id)
    {
        if (id == Guid.Empty)
            throw new DepartamentArgumentException();

        if (userId == Guid.Empty)
            throw new UserArgumentException();

        var res = await _departmentsRepository.GetDepartmentByIdAsync(id);

        if (res is null)
            throw new DepartmentNotFoundException();

        var departmentUsers = await _usersRepository.GetUsersByDepartmentIdAsync(res.Id);
        
        return res.MapToFullInfoDto(departmentUsers);
    }

    // public async Task<List<GetDepartmentResponseDto>> GetAllAsync(Guid userId)
    // {
    //     var user = await _usersRepository.GetByIdAsync(userId);
    //
    //     if (user is null)
    //     {
    //         throw new UserNotFoundRequest();
    //     }
    //
    //     if (user.DepartmentId is null)
    //     {
    //         throw new UserNotFoundRequest();
    //     }
    //     
    //     var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);
    //
    //     if (department is null)
    //     {
    //         throw new DepartmentNotFoundRequest();
    //     }
    //     
    //     var res = await _departmentsRepository.GetAllDepartmentsByCompanyIdAsync(department.CompanyId);
    //
    //
    //     return res.MapToDto();
    // }

    public async Task<GetDepartmentResponseDto> UpdateAsync(Guid userId, Guid id, UpdateDepartmentRequestDto updateDepartmentRequestDto)
    {
        if (id == Guid.Empty)
            throw new DepartamentArgumentException();

        if (userId == Guid.Empty)
            throw new UserArgumentException();


        if (await _departmentsRepository.GetDepartmentByIdAsync(id) is null)
            throw new DepartmentNotFoundException();
        
        if (await _usersRepository.GetByIdAsync(updateDepartmentRequestDto.SupervisorId) is null)
            throw new SupervisorNotFoundRequest();
        
        Image? image = null;
        if (updateDepartmentRequestDto.ImageId is not null)
        {
            image = await _fileServiceClient.GetImageByIdAsync((Guid)updateDepartmentRequestDto.ImageId);
            
            if (image is null)
                throw new FileNotFoundException();
        }
        
        var dbDepartment = updateDepartmentRequestDto.MapToDb(image?.Name);

        var res = await _departmentsRepository.UpdateDepartmentAsync(id, dbDepartment);
        return res.MapToDto();
    }

    public async Task DeleteAsync(Guid userId, Guid id)
    {
        if (id == Guid.Empty)
            throw new DepartamentArgumentException();

        if (await _departmentsRepository.GetDepartmentByIdAsync(id) is null)
            throw new DepartmentNotFoundException();
        
        await _departmentsRepository.DeleteDepartmentAsync(id);
    }

    public async Task<List<GetDepartmentResponseDto>> GetChidrenDepartments(Guid departmentId)
    {
        if (departmentId == Guid.Empty)
            throw new DepartamentArgumentException();

        var res = await _departmentsRepository.GetDepartmentsByParentIdAsync(departmentId);
        return res.MapToDto();
    }

    public async Task<List<GetUserResponseDto>> GetDepartmentUsersAsync(Guid departmentId)
    {
        if (departmentId == Guid.Empty)
            throw new DepartamentArgumentException();

        var res = await _usersRepository.GetUsersByDepartmentIdAsync(departmentId);
        return res.Select(u => u.MapToDto()).ToList();
    }

    public async Task<List<GetDepartmentResponseDto>> GetUserDepartmentsAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        var user = await _usersRepository.GetByIdAsync(userId);
        
        if (user is null)
            throw new UserNotFoundException();
        
        var res = await _departmentsRepository.GetDepartmentsByParentIdAsync((Guid)user.DepartmentId);
        return res.MapToDto();
    }
}