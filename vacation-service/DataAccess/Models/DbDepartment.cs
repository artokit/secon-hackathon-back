using Common.Roles;

namespace DataAccess.Models;

public class DbDepartment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? SupervisorId { get; set; }
    public Guid? ParentDepartmentId {get; set;}
    public string? ImageName { get; set; }
    public DepartmentRoles Role { get; set; }
}