using System.Text.Json.Serialization;

namespace Common.Roles;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DepartmentRoles
{
    Management,  // Управление
    Department,  // Департамент
    Division,    // Отдел
    Sector       // Сектор
}