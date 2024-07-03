using BlazorStoreApp.Infraestructura.Dtos;

namespace MinimalAPIERP.Servicios
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
    }
}