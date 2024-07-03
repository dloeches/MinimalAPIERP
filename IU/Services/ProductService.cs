using BlazorStoreApp.Infraestructura.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace IU.Services
{
    public class ProductService:IProductService
    {
        private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuracion;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
          //  _configuracion = configuracion;
     
            httpClient.BaseAddress = new Uri("https://localhost:54528/");
        }

        public async Task<ProductsResponse> GetProductsAsync(int page, int pageSize)
        { 
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:54528/erp/products?pageSize={pageSize}&page={page}");
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<ProductsResponse>();
                return data;
            }                  
        }
    }
}