using ERP.Data;
using ERP;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPIERP.Servicios
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int?> GetCategoryIdByGuidAsync(Guid categoryGuid)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryGuid == categoryGuid);
            if (category != null)
            {
                return category.CategoryId;
            }
            return null;
        }
    }
}
