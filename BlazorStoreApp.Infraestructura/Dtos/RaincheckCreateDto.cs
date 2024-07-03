namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record RaincheckCreateDto
    {
        public string? Name { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; } = 0;
        public double SalePrice { get; set; } = 0;
        public Guid StoreId { get; set; }
    }
}