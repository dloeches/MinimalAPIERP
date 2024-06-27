using ERP;

namespace MinimalAPIERP.Servicios
{
    public interface ICategoryService
    {
        Task<int?> GetCategoryIdByGuidAsync(Guid categoryGuid);
    }
}