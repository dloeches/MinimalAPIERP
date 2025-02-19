﻿namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record OrderDetailDto
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductDto? Product { get; set; }
        //public OrderDto? Order { get; set; }
    }
}