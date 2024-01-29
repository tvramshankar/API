using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Charecter
{
    Ram = 1,
    Sam = 2,
    Swathi = 3
}