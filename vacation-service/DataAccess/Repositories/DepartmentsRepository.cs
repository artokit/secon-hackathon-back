using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private IDapperContext _dapperContext;

    public DepartmentsRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    } 
        
    public async Task<DbDepartment?> GetDepartmentByIdAsync(Guid id)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", parent_department_id as ""ParentDepartmentId"", image_name as ""ImageName"", role as ""Role"" FROM departments
                WHERE id = @id",
            new { id });
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }

    public async Task<List<DbDepartment>> GetDepartmentsByParentIdAsync(Guid parentId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", parent_department_id as ""ParentDepartmentId"", image_name as ""ImageName"", role as ""Role"" FROM departments
                WHERE parent_department_id = @parentId",
            new { parentId });
        
        return await _dapperContext.ListOrEmpty<DbDepartment>(queryObject);
    }

    public async Task<DbDepartment> GetDepartmentByParentIdAsync(Guid parentId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", parent_department_id as ""ParentDepartmentId"", image_name as ""ImageName"", role as ""Role"" FROM departments
                WHERE parent_department_id = @parentId",
            new { parentId });
        
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment> CreateDepartmentAsync(DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO departments (name, description, supervisor_id, parent_department_id, role) VALUES (@Name, @Description, @SupervisorId, @ParentDepartmentId, @role)
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", parent_department_id as ""ParentDepartmentId"", image_name as ""ImageName"", role as ""Role""",
            new
            {
                department.Id,
                department.Name,
                department.Description,
                department.SupervisorId,
                department.ParentDepartmentId,
                department.Role
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment> UpdateDepartmentAsync(Guid departmentId, DbDepartment department)
    {
        var queryObject = new QueryObject(
            @"UPDATE departments SET name = @Name, description = @Description, supervisor_id = @SupervisorId, image_name = @ImageName
                WHERE id=@id
                RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""SupervisorId"", parent_department_id as ""ParentDepartmentId"", image_name as ""ImageName"", role as ""Role""",
            new
            {
                id = departmentId, name = department.Name, description = department.Description,
                supervisorId = department.SupervisorId, ImageName = department.ImageName
            });
        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }

    public async Task DeleteDepartmentAsync(Guid id)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM departments WHERE id = @id",
            new { id });
        await _dapperContext.Command(queryObject);
    }
}