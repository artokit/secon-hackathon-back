using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Images;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Application.Common.Interfaces;
using Application.FileService.Models;
using Common;
using Common.Roles;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;


public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly INotificationServiceClient _notificationServiceClient;
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly IFileServiceClient _fileServiceClient;
    
    public UserService(IUsersRepository usersRepository, INotificationServiceClient notificationServiceClient, IDepartmentsRepository departmentsRepository, IFileServiceClient fileServiceClient)
    {
        _usersRepository = usersRepository;
        _notificationServiceClient = notificationServiceClient;
        _departmentsRepository = departmentsRepository;
        _fileServiceClient = fileServiceClient;
    }

    public async Task<GetUserResponseDto> GetMe(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        var res = await _usersRepository.GetByIdAsync(userId);
        
        if (res is null)
        {
            throw new UserNotFoundException();
        }

        var userDto = res.MapToGetMeDto();
        
        // if (res.DepartmentId is not null)
        // {
        //     var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)res.DepartmentId);
        // }

        return userDto;
    }
    
    public async Task<GetUserResponseDto> CreateAsync(Guid userId, CreateUserRequestDto request)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        if (request == null)
            throw new ArgumentNullException(nameof(CreateUserRequestDto));

        // Валидация данных
        if (await _usersRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailIsExistException();
        }

        var departmentTask = _departmentsRepository.GetDepartmentByIdAsync(request.DepartmentId);
        var userTask = _usersRepository.GetByIdAsync(userId);
        
        await Task.WhenAll(departmentTask, userTask);

        var user = userTask.Result;
        var department = departmentTask.Result;

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        if (department is null)
        {
            throw new DepartmentNotFoundException();
        }

        if (department.SupervisorId != userId && user.UserRole != UserRoles.Director && user.UserRole != UserRoles.Hr)
        {
            throw new CantAddUserToAnotherDepartment();
        }

        
        var inviterUserDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);
        
        var generatedPassword = PasswordService.GeneratePassword();
        Console.WriteLine(generatedPassword);
        var res = await _usersRepository.AddAsync(request.MapToDb(generatedPassword));
        return res.MapToDto();
    }

    public async Task DeleteAsync(Guid userId, Guid employeeId)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        if (employeeId == Guid.Empty)
            throw new UserArgumentException();


        var user = await _usersRepository.GetByIdAsync(userId);

        if (user.UserRole != UserRoles.Director && user.UserRole != UserRoles.Hr)
        {
            throw new CantDeleteUserException();
        }

        var userToDelete = await _usersRepository.GetByIdAsync(employeeId);
        
        if (userToDelete is null)
        {
            throw new UserNotFoundException();
        }

        var departmentUserToDeleteTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)userToDelete.DepartmentId!);
        var departmentUserTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);

        await Task.WhenAll(departmentUserToDeleteTask, departmentUserTask);

        // var departmentUser = departmentUserTask.Result;
        // var departmentUserToDelete = departmentUserToDeleteTask.Result;
        
        await _usersRepository.DeleteAsync(employeeId);
    }

    public async Task<GetUserResponseDto> UpdateAsync(Guid userId, UpdateUserRequestDto request)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        if (request == null)
            throw new ArgumentNullException(nameof(UpdateUserRequestDto));

        var user = await _usersRepository.GetByIdAsync(userId);
        var userToUpdate = await _usersRepository.GetByIdAsync(request.UserId);
        
        // Проверка на сущесвование пользователей.
        if (user is null || userToUpdate is null)
        {
            throw new UserNotFoundException();
        }

        if (user.UserRole is not UserRoles.Director && user.UserRole is not UserRoles.Hr && user.Id != request.UserId)
        {
            throw new CantEditUserException();
        }
        
        var currentDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)userToUpdate.DepartmentId!);
        var userDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);
        
        await Task.WhenAll(currentDepartmentTask, userDepartmentTask);

        var currentDepartment = currentDepartmentTask.Result;
        var userDepartment = userDepartmentTask.Result;

        if (currentDepartment is null || userDepartment is null)
        {
            throw new DepartmentNotFoundException();
        }
        
        // Проверка на то, что пользователю хотят поменять отдел.
        if (request.DepartmentId is not null)
        {
            // Проверка, что у человека есть права на то, чтобы поменять отдел.
            if (user.UserRole is not UserRoles.Director && user.UserRole is not UserRoles.Hr)
            {
                throw new CantEditDepartmentException();
            }
            
            var updatedDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)request.DepartmentId!);

            if (updatedDepartment is null)
            {
                throw new DepartmentNotFoundException();
            }
        }

        
        Image? image = null;
        
        if (request.ImageId is not null)
        {
            image = await _fileServiceClient.GetImageByIdAsync((Guid)request.ImageId);
            
            if (image is null)
            {
                throw new ImageNotFoundException();
            }
        }
        
        var dbUserToUpdate = request.MapToDb(userToUpdate, image?.Name);
        var updatedUser = await _usersRepository.UpdateAsync(dbUserToUpdate);
        
        return updatedUser.MapToDto();
    }
}