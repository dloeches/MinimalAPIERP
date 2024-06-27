using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using MinimalAPIERP.Dtos;
using MinimalAPIERP.Servicios;

namespace ERP.Api;

internal static class ProductApi
{
    public static RouteGroupBuilder MapProductApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Product Api");

        group.MapGet("/products", async Task<Results<Ok<IList<ProductDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
           mapper.Map<List<ProductDto>>(await db.Products
                .Include(p => p.Category)
                .ToListAsync())
           is IList<ProductDto> stores
            ? TypedResults.Ok(stores)
            : TypedResults.NotFound())
       .WithName("GetProducts")
       .WithOpenApi();

        group.MapGet("/product/{storeGuid}/{productGuid}", async Task<Results<Ok<ProductDto>, NotFound>> (Guid storeGuid, Guid productGuid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<ProductDto>(await db.Products
                .Include(p => p.Rainchecks)
                    .ThenInclude(s => s.Store)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductGuid == productGuid && m.Rainchecks.Any(r => r.Store.StoreGuid == storeGuid)))
                is ProductDto product
                    ? TypedResults.Ok(product)
                    : TypedResults.NotFound();
        })
        .WithName("GetProductoByStore")
        .WithOpenApi();

        group.MapGet("/product/{productGuid}", async Task<Results<Ok<ProductDto>, NotFound>> (Guid productGuid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<ProductDto>(await db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductGuid == productGuid))
                is ProductDto product
                    ? TypedResults.Ok(product)
                    : TypedResults.NotFound();
        })
        .WithName("GetProduct")
        .WithOpenApi();

        group.MapPost("/products", async Task<Created<ProductDto>> (ProductCreateDto productDto, IProductService productService) =>
        {
            var product = await productService.CreateProductAsync(productDto);

            return TypedResults.Created($"/products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .WithOpenApi();

        group.MapPut("/products/{guid}", async (Guid guid, ProductDto productDto, AppDbContext db, IMapper mapper, IProductService productService) =>
        {
            return await productService.UpdateProductAsync(guid,productDto);

        })
        .WithName("UpdateProduct")
        .WithOpenApi();

        group.MapDelete("products/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.Products.Where(p => p.ProductGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteProduct")
        .WithOpenApi();

        return group;
    }
}