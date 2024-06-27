using ERP;

namespace MinimalAPIERP.Servicios
{
    public interface IStoreService
    {
        Task<int?> GetStoreIdByGuidAsync(Guid storeGuid);
    }
}