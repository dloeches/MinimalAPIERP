using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using MinimalAPIERP.Dtos;
using Microsoft.CodeAnalysis;
using MinimalAPIERP.Servicios;

namespace ERP.Api;

internal static class RaincheckApi
{
    public static RouteGroupBuilder MapRaincheckApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Raincheck Api");

        group.MapGet("/raincheck", async Task<Results<Ok<List<RaincheckDto>>, NotFound>> (AppDbContext db, IMapper mapper,
                                                                       Guid? storeGuid, int pageSize = 10, int page = 0) =>
        {
            var query = db.Rainchecks.AsQueryable();

            if (storeGuid.HasValue)
            {
                query = query.Where(r => r.Store.StoreGuid == storeGuid.Value);
            }

            var data = mapper.Map<List<RaincheckDto>>(await query
                    .OrderBy(s => s.RaincheckId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Include(s => s.Product)
                    .ThenInclude(p => p.Category)
                    .Include(s => s.Store)
                    .ToListAsync());


            return data.Any()
                ? TypedResults.Ok(data)
                : TypedResults.NotFound();
        })
       .WithName("GetRainchecks")
       .WithOpenApi();

        group.MapGet("/raincheck/{storeGuid}/{productGuid}", async Task<Results<Ok<List<RaincheckDto>>, NotFound>> (AppDbContext db,IMapper mapper,Guid storeGuid, Guid productGuid) =>
        {
            var data = mapper.Map<List<RaincheckDto>>(await 
                db.Rainchecks
                .Where(r => r.Store.StoreGuid == storeGuid && r.Product.ProductGuid == productGuid)
                .OrderBy(s => s.RaincheckId)
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.Store)
                .ToListAsync());

            return data.Any()
                ? TypedResults.Ok(data)
                : TypedResults.NotFound();
        })
        .WithName("GetRaincheckByStore&Product")
        .WithOpenApi();

        group.MapPost("/rainchecks", async (RaincheckCreateDto raincheckCreateDto, AppDbContext db, IMapper mapper, IRaincheckService raincheckService) =>
        {
            var raincheck = await raincheckService.CreateRaincheckAsync(raincheckCreateDto);

            return TypedResults.Created($"/rainchecks/{raincheck.Id}", raincheck);
        })
        .WithName("CreateRaincheck")
        .WithOpenApi();

        group.MapPut("/rainchecks/{guid}", async (Guid guid, ProductDto productDto, AppDbContext db, IMapper mapper, IProductService productService) =>
        {
            return await productService.UpdateProductAsync(guid, productDto);

        })
        .WithName("UpdateRaincheck")
        .WithOpenApi();

        group.MapDelete("rainchecks/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.Rainchecks.Where(r => r.RaincheckGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteRaincheck")
        .WithOpenApi();

        return group;
    }
}