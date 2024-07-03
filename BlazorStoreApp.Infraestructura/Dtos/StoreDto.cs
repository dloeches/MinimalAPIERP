namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record StoreDto
    {
        public Guid Id { get; set; }= Guid.NewGuid();   
        public string? Name { get; set; }
    }
}