namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record CartItemDto
    {
        public Guid Id { get; set; }
        public string? CartId { get; set; } 
        public int Count { get; set; }
        public ProductDto? Product { get; set; }
    }
}