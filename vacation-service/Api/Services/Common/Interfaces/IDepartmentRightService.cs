using Api.Dto.Departments.Responses;
using Common.Roles;

namespace Api.Services.Common.Interfaces;

public interface IDepartmentRightService
{
    public List<DepartmentRoles> GetLowerDepartmentRoles(DepartmentRoles departmentRoles);
}