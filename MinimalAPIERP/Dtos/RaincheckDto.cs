namespace MinimalAPIERP.Dtos
{
    public class RaincheckDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreDto? Store { get; set; }
        public ProductDto? Product { get; set; }
    }
}