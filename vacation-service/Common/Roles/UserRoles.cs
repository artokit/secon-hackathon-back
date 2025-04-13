using System.Text.Json.Serialization;

namespace Common.Roles;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoles
{
    Director,
    Hr,
    Worker
}