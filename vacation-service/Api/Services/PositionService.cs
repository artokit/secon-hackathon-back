using Api.Dto.Orders.Requests;
using Api.Dto.Positions.Requests;
using Api.Dto.Positions.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Positions;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;

namespace Api.Services;

public class PositionService : IPositionService
{
    private IPositionsRepository _positionsRepository;
    private IUsersRepository _usersRepository;
    private IDepartmentsRepository _departmentsRepository;

    public PositionService(IPositionsRepository positionsRepository, IUsersRepository usersRepository, IDepartmentsRepository departmentsRepository)
    {
        _positionsRepository = positionsRepository;
        _usersRepository = usersRepository;
        _departmentsRepository = departmentsRepository;
    }
    
    public async Task<List<GetPositionResponseDto>> GetPositionsByDepartmentIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new UserArgumentException();

        var user = await _usersRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (user.DepartmentId is null)
        {
            throw new DepartmentNotFoundException();
        }
        
        
        var res = await _positionsRepository.GetByDepartmentIdAsync((Guid)user.DepartmentId);

        return res.MapToDto();
    }

    public async Task<GetPositionResponseDto> AddPositionAsync(CreatePositionRequestDto createPositionRequestDto)
    {
        if (createPositionRequestDto == null)
            throw new ArgumentNullException(nameof(CreateOrderRequestDto));

        var department = await _departmentsRepository.GetDepartmentByIdAsync(createPositionRequestDto.DepartmentId);
        if (department is null)
        {
            throw new DepartmentNotFoundException();
        }
        
        var res = await _positionsRepository.AddPositionAsync(createPositionRequestDto.MapToDb());
        return res.MapToDto();
    }

    public async Task<GetPositionResponseDto> UpdatePositionAsync(UpdatePositionRequestDto updatePositionRequestDto)
    {
        if (updatePositionRequestDto == null)
            throw new ArgumentNullException(nameof(UpdateOrderRequestDto));

        var positionsToUpdate = await _positionsRepository.GetPositionByIdAsync(updatePositionRequestDto.Id);
        if (positionsToUpdate is null)
        {
            throw new PositionNotFoundException();
        }

        if (updatePositionRequestDto.DepartmentId is not null)
        {
            var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)updatePositionRequestDto.DepartmentId);
            if (department is null)
            {
                throw new DepartmentNotFoundException();
            }
        }

        var res = await _positionsRepository.UpdatePositionAsync(updatePositionRequestDto.MapToDb(positionsToUpdate));
        return res.MapToDto();
    }

    public async Task DeletePositionAsync(Guid positionsId)
    {
        if (positionsId == Guid.Empty)
            throw new PositionArgimentException();

        var positionsToUpdate = await _positionsRepository.GetPositionByIdAsync(positionsId);
        if (positionsToUpdate is null)
        {
            throw new PositionNotFoundException();
        }
        await _positionsRepository.DeletePositionAsync(positionsId);
    }
}