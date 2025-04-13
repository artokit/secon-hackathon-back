using Common;
using Common.Roles;

namespace DataAccess.Models;

public class DbUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public UserRoles UserRole { get; set; }
    public string? ImageName { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
    public DateTime HiringDate { get; set; }
    public int TabelNumber { get; set; }
    public string PositionName { get; set; }
}