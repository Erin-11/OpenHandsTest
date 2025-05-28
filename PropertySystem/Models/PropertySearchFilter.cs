namespace PropertySystem.Models
{
    public class PropertySearchFilter
    {
        public string? Region { get; set; }
        public string? District { get; set; }
        public string? PropertyType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}