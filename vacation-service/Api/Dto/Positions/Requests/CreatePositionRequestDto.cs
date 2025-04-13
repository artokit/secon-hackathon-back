using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Positions.Requests;

public class CreatePositionRequestDto
{
    [Required(ErrorMessage = "Необходимо назначить указать id отдела.")]
    public Guid DepartmentId { get; set; }
    
    [Required(ErrorMessage = "Наименование должности обязательно для заполнения.")]
    [MinLength(1, ErrorMessage = "Наименование должности не может быть пустым.")]
    public  string Name { get; set; }
}