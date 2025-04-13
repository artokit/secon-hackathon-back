namespace Api.Dto.Positions.Responses;

public class GetPositionResponseDto
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public  string Name { get; set; }
}