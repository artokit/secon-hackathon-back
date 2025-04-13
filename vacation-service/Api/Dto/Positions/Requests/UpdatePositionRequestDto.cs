namespace Api.Dto.Positions.Requests;

public class UpdatePositionRequestDto
{
    public Guid Id { get; set; }
    public Guid? DepartmentId { get; set; }
    public  string? Name { get; set; }
}