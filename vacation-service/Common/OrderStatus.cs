using System.Text.Json.Serialization;

namespace Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Collecting,
    WaitingApprove,
    Approved
}