using System.Text.Json.Serialization;

namespace Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventTypeEnum
{
    deposit,
    withdraw,
    transfer
}