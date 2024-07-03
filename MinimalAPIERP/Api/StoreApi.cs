using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using BlazorStoreApp.Infraestructura.Dtos;

namespace ERP.Api;

internal static class StoreApi
{
    public static RouteGroupBuilder MapStoreApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Store Api");

       group.MapGet("/stores", async Task<Results<Ok<IList<StoreDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            mapper.Map<List<StoreDto>>(await db.Stores.ToListAsync())
                is IList<StoreDto> stores
                    ? TypedResults.Ok(stores)
                    : TypedResults.NotFound())
        .WithName("GetStores")
        .WithOpenApi();

        group.MapGet("/store/{guid}", async Task<Results<Ok<StoreDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<StoreDto>(await db.Stores.FirstOrDefaultAsync(m => m.StoreGuid == guid))
                is StoreDto store
                    ? TypedResults.Ok(store)
                    : TypedResults.NotFound();
        })
        .WithName("GetStore")
        .WithOpenApi();

        group.MapPost("/stores", async Task<Created<StoreDto>>(StoreDto storedto, AppDbContext db, IMapper mapper) =>
        {
            var store = mapper.Map<Store>(storedto);
            db.Stores.Add(store);
            await db.SaveChangesAsync();
                        
            return TypedResults.Created($"/stores/{store.StoreGuid}", mapper.Map<StoreDto>(store));
        })
        .WithName("CreateStore")
        .WithOpenApi();

        group.MapPut("/stores/{guid}", async Task<Results<Ok, NotFound, BadRequest>> (Guid guid, StoreDto storeDto, AppDbContext db) =>
        {
            if (guid != storeDto.Id)
            {
                return TypedResults.BadRequest();
            }

            var rowsAffected = await db.Stores.Where(t => t.StoreGuid == guid)
                        .AsNoTracking()
                        .ExecuteUpdateAsync(updates =>
                                            updates.SetProperty(t => t.Name, storeDto.Name));

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("UpdateStore")
        .WithOpenApi();

        group.MapDelete("stores/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.Stores.Where(s => s.StoreGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteStore")
        .WithOpenApi();

        return group;
    }
}