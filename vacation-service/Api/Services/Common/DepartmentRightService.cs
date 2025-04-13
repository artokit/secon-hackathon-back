using Api.Exceptions.Departments;
using Common.Roles;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using IDepartmentRightService = Api.Services.Common.Interfaces.IDepartmentRightService;

namespace Api.Services.Common;

public class DepartmentRightService : IDepartmentRightService
{
    private readonly IDepartmentsRepository _departmentsRepository;
    
    public DepartmentRightService(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    public List<DepartmentRoles> GetLowerDepartmentRoles(DepartmentRoles departmentRoles)
    {
        if (departmentRoles == DepartmentRoles.Management)
        {
            return [DepartmentRoles.Department, DepartmentRoles.Division, DepartmentRoles.Sector];
        }
        
        if (departmentRoles == DepartmentRoles.Department)
        {
            return [DepartmentRoles.Division, DepartmentRoles.Sector];
        }

        if (departmentRoles == DepartmentRoles.Division)
        {
            return [DepartmentRoles.Sector];
        }

        return [];
    }

    public async Task<List<DbDepartment>> GetAllParentDepartmentsAsync(Guid departmentId)
    {
        var currentDepartment = await _departmentsRepository.GetDepartmentByIdAsync(departmentId);
        
        var departments = new List<DbDepartment>();
        
        while (currentDepartment != null && currentDepartment.ParentDepartmentId != null)
        {
            departments.Add(currentDepartment);
            currentDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)currentDepartment.ParentDepartmentId);
        }

        return departments;
    }

    public void CheckDepartmentIsExist(DbDepartment? department)
    {
        if (department is null)
        {
            throw new DepartmentNotFoundException();
        }
    }
}