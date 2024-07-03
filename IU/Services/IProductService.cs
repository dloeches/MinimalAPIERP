using BlazorStoreApp.Infraestructura.Dtos;

namespace IU.Services
{
    public interface IProductService
    {
        Task<ProductsResponse> GetProductsAsync(int page, int pageSize);
    }
}