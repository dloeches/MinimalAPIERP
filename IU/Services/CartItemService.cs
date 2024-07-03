using BlazorStoreApp.Infraestructura.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace IU.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuracion;

        public CartItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          //  _configuracion = configuracion;
     
            httpClient.BaseAddress = new Uri("https://localhost:54528/");
        }
        
        public async Task<List<CartItemDto>> GetCartItemsAsync()
        { 
            return await _httpClient.GetFromJsonAsync<List<CartItemDto>>($"/erp/cartitems");
        }

        public async Task<CartItemDto> AddToCartAsync(CartItemDto cartItemDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:54528/erp/cartitems", cartItemDto);
            response.EnsureSuccessStatusCode();

            var createdItem = await response.Content.ReadFromJsonAsync<CartItemDto>();
            return createdItem;
        }

        public async Task UpdateCartItemAsync(CartItemDto cartItemDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"erp/cartitems/{cartItemDto.Id}", cartItemDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCartItemAsync(Guid cartItemId)
        {
            var response = await _httpClient.DeleteAsync($"erp/cartitems/{cartItemId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task ClearCartByCartAsync(string cartId)
        {
            var response = await _httpClient.DeleteAsync($"erp/cartitems/delete/{cartId}");
            response.EnsureSuccessStatusCode();
        }
    }
}