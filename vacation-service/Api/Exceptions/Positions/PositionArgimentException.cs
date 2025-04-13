namespace Api.Exceptions.Positions
{
    public class PositionArgimentException : ArgumentException
    {
        public PositionArgimentException(string? message = "Position ID cannot be null.") : base(message) { }
    }
}
