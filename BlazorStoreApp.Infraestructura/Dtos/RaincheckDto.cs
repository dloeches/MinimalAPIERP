namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record RaincheckDto
    {
        public Guid Id { get; set; }= Guid.NewGuid();   
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreDto? Store { get; set; }
        public ProductDto? Product { get; set; }
    }
}