using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ERP.Data;
using BlazorStoreApp.Infraestructura.Dtos;

namespace ERP.Api;

internal static class OrderDetailsApi
{
    public static RouteGroupBuilder MapOrderDetailsApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Order Details Api");

        group.MapGet("/orderdetails", async Task<Results<Ok<List<OrderDetailDto>>, NotFound>> (AppDbContext db, IMapper mapper,int pageSize = 10, int page = 0) =>
        {
            var query = db.OrderDetails.AsQueryable();
                    
            var data = mapper.Map<List<OrderDetailDto>>(await query
                    .OrderBy(od => od.OrderId)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Include(od => od.Order)
                    .ToListAsync());

            return data.Any()
                ? TypedResults.Ok(data)
                : TypedResults.NotFound();
        })
        .WithName("GetOrderDetails")
        .WithOpenApi();

        group.MapGet("/orderdetails/{Guid}", async Task<Results<Ok<OrderDetailDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            return mapper.Map<OrderDetailDto>(await db.OrderDetails 
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.OrderDetailGuid == guid))
                is OrderDetailDto orderDetail
                    ? TypedResults.Ok(orderDetail)
                    : TypedResults.NotFound();
        })
        .WithName("GetOrderDetail")
        .WithOpenApi();

        group.MapPost("/orderdetails", async (OrderDetailDto orderDetailDto, AppDbContext db, IMapper mapper) => {

            var orderDetail = mapper.Map<OrderDetail>(orderDetailDto);
            db.OrderDetails.Add(orderDetail);
            await db.SaveChangesAsync();

            return Results.Created($"/orderdetails/{orderDetail.OrderDetailId}", mapper.Map<OrderDetailDto>(orderDetail));
        })
        .WithName("CreateOrderDetail")
        .WithOpenApi();
           
        group.MapDelete("orderdetails/{guid}", async Task<Results<NotFound, Ok>> (Guid guid, AppDbContext db) =>
        {
            var rowsAffected = await db.OrderDetails.Where(p => p.OrderDetailGuid == guid)
                .AsNoTracking()
                .ExecuteDeleteAsync();

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        })
        .WithName("DeleteOrderDetail")
        .WithOpenApi();

        return group;
    }
}