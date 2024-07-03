namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record ProductsResponse
    {
        public List<ProductDto>? Productos { get; set; }
        public int TotalPages { get; set; }
    }
}