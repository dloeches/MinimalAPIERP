
using BlazorStoreApp.Infraestructura.Dtos;

namespace IU.Services
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetCartItemsAsync();
        Task<CartItemDto> AddToCartAsync(CartItemDto cartItemDto);
        Task UpdateCartItemAsync(CartItemDto cartItemDto);
        Task DeleteCartItemAsync(Guid guid);
        Task ClearCartByCartAsync(string cartId);
    }
}