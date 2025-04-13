using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Positions;

public class PositionNotFoundException : NotFoundRequestException
{
    public PositionNotFoundException(string? message = "Должность не найдена") : base(message) { }
}