namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record ProductCreateDto
    {
        public string? Name { get; set; }
        public string? SkuNumber { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? ProductArtUrl { get; set; }
        public string ProductDetails { get; set; } = null!;        
        public Guid CategoryGuid { get; set; }
    }
}