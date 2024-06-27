﻿namespace MinimalAPIERP.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? SkuNumber { get; set; }
        public string Description { get; set; } = null!;
        public decimal SalePrice { get; set; }      
        public CategoryDto? Category { get; set; }
    }
}