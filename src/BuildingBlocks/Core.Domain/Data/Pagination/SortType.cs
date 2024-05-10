using System.Text.Json.Serialization;

namespace Core.Domain.Data.Pagination
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortType
    {
        [JsonPropertyName("Asc")]
        Asc = 1,

        [JsonPropertyName("Desc")]
        Desc = 2
    }
}