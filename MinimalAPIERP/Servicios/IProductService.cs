﻿using Microsoft.AspNetCore.Http.HttpResults;
using BlazorStoreApp.Infraestructura.Dtos;

namespace MinimalAPIERP.Servicios
{
    public interface IProductService
    {
        Task<int?> GetProductIdAsync(Guid productGuid);
        Task<ProductDto> CreateProductAsync(ProductCreateDto productDto);
        Task<Results<Ok, NotFound, BadRequest>> UpdateProductAsync(Guid guid, ProductDto productDto);
    }
}