namespace MinimalAPIERP.Dtos
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public string? CartId { get; set; } 
        public int Count { get; set; }
        public ProductDto? Product { get; set; }
    }
}