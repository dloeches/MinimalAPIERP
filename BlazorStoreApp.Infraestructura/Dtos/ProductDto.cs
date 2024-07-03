using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? SkuNumber { get; set; }
        public string Description { get; set; } = null!;
        public decimal SalePrice { get; set; }
        public decimal Price { get; set; }
        public string? ProductArtUrl { get; set; }
        public string? ProductDetails { get; set; }
        public int Inventory { get; set; }
        public int LeadTime { get; set; }
        public CategoryDto? Category { get; set; }
    }
}