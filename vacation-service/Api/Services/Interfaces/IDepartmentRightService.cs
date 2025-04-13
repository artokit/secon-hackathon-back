using Common.Roles;
using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface IDepartmentRightService
{
    public List<DepartmentRoles> GetLowerDepartmentRoles(DepartmentRoles departmentRoles);
    public Task<List<DbDepartment>> GetAllParentDepartmentsAsync(Guid departmentId);
}