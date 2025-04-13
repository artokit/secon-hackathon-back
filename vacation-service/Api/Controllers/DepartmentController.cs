using Api.Controllers.Abstractions;
using Api.Dto.Departments.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IDepartmentRightService = Api.Services.Common.Interfaces.IDepartmentRightService;

namespace Api.Controllers;

[Route("vacation-service/departments")]
public class DepartmentController : BaseController
{
    private readonly IDepartmentService _departmentService;
    private readonly IDepartmentRightService _departmentRightService;

    public DepartmentController(IDepartmentService departmentService, IDepartmentRightService departmentRightService)
    {
        _departmentService = departmentService;
        _departmentRightService = departmentRightService;
    }
    
    [HttpPost]
    [Authorize(Roles = "Hr,Director")]
    public async Task<IActionResult> Create(CreateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.CreateAsync(UserId, departmentRequestDto));
    }

    [HttpGet("{departmentId}")]
    public async Task<IActionResult> GetById(Guid departmentId)
    {
        return Ok(await _departmentService.GetByIdAsync(UserId, departmentId));
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     return Ok(await _departmentService.GetAllAsync(UserId));
    // }

    [HttpPatch("{departmentId}")]
    public async Task<IActionResult> Update(Guid departmentId, UpdateDepartmentRequestDto departmentRequestDto)
    {
        return Ok(await _departmentService.UpdateAsync(UserId, departmentId, departmentRequestDto));
    }

    [HttpDelete("{departmentId}")]
    public async Task<IActionResult> Delete(Guid departmentId)
    {
         await _departmentService.DeleteAsync(UserId, departmentId);
         return NoContent();
    }

    [HttpGet("{departmentId}/children")]
    public async Task<IActionResult> GetChildren(Guid departmentId)
    {
        var res = await _departmentService.GetChidrenDepartments(departmentId);
        return Ok(res);
    }

    [HttpGet("{departmentId}/users")]
    public async Task<IActionResult> GetDepartmentUsers(Guid departmentId)
    {
        var res = await _departmentService.GetDepartmentUsersAsync(departmentId);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserDepartments()
    {
        var res = await _departmentService.GetUserDepartmentsAsync(UserId);
        return Ok(res);
    }

    // [HttpGet]
    // public async Task<IActionResult> GetDepartmentsArchitecture()
    // {
    //     var res = await _departmentRightService.GetDepartmentsArchitectureAsync(UserId);
    //     return Ok(res);
    // }
}