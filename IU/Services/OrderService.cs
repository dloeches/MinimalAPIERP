using BlazorStoreApp.Infraestructura.Dtos;

namespace IU.Services
{
    public class OrderService: IOrderService

    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:54528/erp/orders", order);
            response.EnsureSuccessStatusCode();

            var createdItem = await response.Content.ReadFromJsonAsync<OrderDto>();
            return createdItem;
        }
    }
}