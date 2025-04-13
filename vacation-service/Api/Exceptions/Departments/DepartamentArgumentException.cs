using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class DepartamentArgumentException : ArgumentException
{
    public DepartamentArgumentException(string? message = "Departament ID cannot be null.") : base(message) { }
}