using BlazorStoreApp.Infraestructura.Dtos;

namespace MinimalAPIERP.Servicios
{
    public interface ICartItemService
    {
        Task<CartItemDto> CreateCartItemAsync(CartItemDto cartItemDto);
    }
}