using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using BlazorStoreApp.Infraestructura.Dtos;
using MinimalAPIERP.Servicios;

namespace ERP.Api;

internal static class OrdersApi
{
    public static RouteGroupBuilder MapOrdersApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Order Api");

        group.MapGet("/orders", async Task<Results<Ok<IList<OrderDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
            mapper.Map<List<OrderDto>>(await db.Orders 
            .Include(c => c.OrderDetails)
            .ToListAsync())
            is IList<OrderDto> orders
            ? TypedResults.Ok(orders)
            : TypedResults.NotFound())
            .WithName("GetOrders")
            .WithOpenApi();

        group.MapGet("/orders/{OrderGuid}", async Task<Results<Ok<OrderDto>, NotFound>> (Guid orderGuid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<OrderDto>(await db.Orders 
                .Include(c => c.OrderDetails)
                .FirstOrDefaultAsync(m => m.OrderGuid == orderGuid))
                is OrderDto Order
                    ? TypedResults.Ok(Order)
                    : TypedResults.NotFound();
        })
        .WithName("GetOrder")
        .WithOpenApi();

        group.MapPost("/orders", async Task<Created<OrderDto>> (OrderDto orderDto, IOrderService orderService) =>
        {
            var order = await orderService.CreateOrderAsync(orderDto);

            return TypedResults.Created($"/orders/{order.Id}", order);
        })
       .WithName("CreateOrder")
       .WithOpenApi();

        group.MapPut("/orders/{id}", async (int id, OrderDto orderDto, AppDbContext db, IMapper mapper) => {
            var order = await db.Orders.FindAsync(id);
            if (order is null) return Results.NotFound();

            mapper.Map(orderDto, order);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateOrder")
        .WithOpenApi();

        group.MapDelete("orders/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.Orders.Where(p => p.OrderGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteOrder")
        .WithOpenApi();

        return group;
    }
}