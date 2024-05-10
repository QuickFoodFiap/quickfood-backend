using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Core.Domain.Data.Pagination
{
    [ExcludeFromCodeCoverage]
    public record class PaginationOptions
    {
        [JsonPropertyName("_page")]
        public int _page { get; set; } = 1;

        [JsonPropertyName("_size")]
        public int _size { get; set; } = 100;

        [JsonPropertyName("_sort")]
        public SortType _sort { get; set; } = SortType.Asc;

        public int GetOffsetSize() => (_page - 1) * _size;
    }
}