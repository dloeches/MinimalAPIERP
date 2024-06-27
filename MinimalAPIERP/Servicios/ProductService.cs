using ERP.Data;
using ERP;
using AutoMapper;
using MinimalAPIERP.Dtos;
using MinimalAPIERP.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class ProductService: IProductService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;
    private readonly ICategoryService _categoryService;

    public ProductService(IMapper mapper, AppDbContext db, ICategoryService categoryService)
    {
        _mapper = mapper;
        _db = db;
        _categoryService = categoryService;
    }

    public async Task<int?> GetProductIdAsync(Guid productGuid)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductGuid == productGuid);
        if (product != null) 
        {
            return product.ProductId;
        }
        return null;
    }

    public async Task<ProductDto> CreateProductAsync(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        var categoryId = await _categoryService.GetCategoryIdByGuidAsync(productDto.CategoryGuid);
        if (categoryId != null)
        {
            product.CategoryId = (int)categoryId;
        }
        else
        {
            throw new InvalidOperationException($"Category with Guid {productDto.CategoryGuid} not found.");
        }

        product.Created = DateTime.Now;

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<Results<Ok, NotFound, BadRequest>> UpdateProductAsync(Guid guid,ProductDto productDto)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductGuid == guid);

        if (guid != productDto.Id)
        {
            return TypedResults.BadRequest();
        }

        var categoryId = await _categoryService.GetCategoryIdByGuidAsync(productDto.Category.Id);
        if (categoryId != null)
        {
            var rowsAffected = await _db.Products.Where(p => p.ProductGuid == guid)
                        .AsNoTracking()
                        .ExecuteUpdateAsync(updates =>
                                                updates.SetProperty(p => p.Title, productDto.Name)
                                                       .SetProperty(p => p.SkuNumber, productDto.SkuNumber)
                                                       .SetProperty(p => p.Description, productDto.Description)
                                                       .SetProperty(p => p.SalePrice, productDto.SalePrice)
                                                       .SetProperty(p => p.CategoryId, categoryId));

            return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
        }
        else
        {
            throw new InvalidOperationException($"Category with Guid {productDto.Id} not found.");
        }

        //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        //.ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
        //.ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
    }
}