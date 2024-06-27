using ERP.Data;
using ERP;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPIERP.Servicios
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _db;

        public StoreService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int?> GetStoreIdByGuidAsync(Guid storeGuid)
        {
            var store = await _db.Stores.FirstOrDefaultAsync(s => s.StoreGuid == storeGuid);
            if (store != null)
            {
                return store.StoreId;
            }
            return null;
        }
    }
}
