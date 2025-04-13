using System.Text.Json.Serialization;

namespace Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RequestStatus
{
    WaitingApprove,
    Approved,
    Unapproved,
}