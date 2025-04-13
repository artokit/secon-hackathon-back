using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class DepartmentNotFoundException : NotFoundRequestException
{
    public DepartmentNotFoundException(string? message = "Отдел не найден") : base(message) { }
}