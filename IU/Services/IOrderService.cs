
using BlazorStoreApp.Infraestructura.Dtos;

namespace IU.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto order);        
    }
}