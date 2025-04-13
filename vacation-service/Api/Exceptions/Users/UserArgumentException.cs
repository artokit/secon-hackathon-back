namespace Api.Exceptions.Users
{
    public class UserArgumentException : ArgumentException
    {
        public UserArgumentException(string? message = "User ID cannot be null.") : base (message) { }
    }
}
