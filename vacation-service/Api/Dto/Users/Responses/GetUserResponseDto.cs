using Common;
using Common.Roles;

namespace Api.Dto.Users.Responses;

public class GetUserResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public Guid? DepartmentId { get; set; }
    public UserRoles UserRole { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? HiringDate { get; set; }
    public string PositionName { get; set; }
}