using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class UserNotFoundException : NotFoundRequestException
{
    public UserNotFoundException(string? message = "Пользователь не найден") : base(message) { }
}