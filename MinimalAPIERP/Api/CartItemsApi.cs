using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using BlazorStoreApp.Infraestructura.Dtos;
using MinimalAPIERP.Servicios;

namespace ERP.Api;

internal static class CartItemsApi
{
    public static RouteGroupBuilder MapCartItemsApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("CartItem Api");

        group.MapGet("/cartitems", async Task<Results<Ok<IList<CartItemDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
           mapper.Map<List<CartItemDto>>(await db.CartItems
                .Include(c => c.Product)
                .ToListAsync())
           is IList<CartItemDto> cartitems
            ? TypedResults.Ok(cartitems)
            : TypedResults.NotFound())
       .WithName("GetCartItems")
       .WithOpenApi();
        
        group.MapGet("/cartitems/{CarItemGuid}", async Task<Results<Ok<CartItemDto>, NotFound>> (Guid carItemGuid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<CartItemDto>(await db.CartItems
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartItemGuid == carItemGuid))
                is CartItemDto cartitem
                    ? TypedResults.Ok(cartitem)
                    : TypedResults.NotFound();
        })
        .WithName("GetCartItem")
        .WithOpenApi();

        group.MapPost("/cartitems", async Task<Created<CartItemDto>> (CartItemDto cartItemDto, ICartItemService cartItemService) =>
        {
            var cartItem = await cartItemService.CreateCartItemAsync(cartItemDto);
            
            return TypedResults.Created($"/cartitems/{cartItem.Id}", cartItem);
        })
        .WithName("CreateCartItem")
        .WithOpenApi();
        
        group.MapPut("/cartitems/{guid}", async (Guid guid, CartItemDto cartItemDto, AppDbContext db, IMapper mapper) => {
            var cartItem = await db.CartItems.Where(c => c.CartItemGuid == guid).FirstAsync();
            if (cartItem is null) return Results.NotFound();

            mapper.Map(cartItemDto, cartItem);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCartItem")
        .WithOpenApi();

        group.MapDelete("cartitems/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.CartItems.Where(p => p.CartItemGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteCartItem")
        .WithOpenApi();

        group.MapDelete("cartitems/delete/{cartId}", async Task<Results<NotFound, Ok>> (string cartId, AppDbContext db) =>
        {
            var rowsAffected = await db.CartItems.Where(p => p.CartId == cartId)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteCartItemsByUser")
        .WithOpenApi();
  
        return group;
    }
}