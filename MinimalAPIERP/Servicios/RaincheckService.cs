using ERP.Data;
using ERP;
using AutoMapper;
using MinimalAPIERP.Dtos;
using MinimalAPIERP.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

public class RaincheckService : IRaincheckService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;
    private readonly IProductService _productService;
    private readonly IStoreService _storeService;
    
    public RaincheckService(IMapper mapper, AppDbContext db, IProductService productService, IStoreService StoreService)
    {
        _mapper = mapper;
        _db = db;
        _productService = productService;
        _storeService = StoreService;
    }

    public async Task<RaincheckDto> CreateRaincheckAsync(RaincheckCreateDto raincheckDto)
    {
        var raincheck = _mapper.Map<Raincheck>(raincheckDto);

        var storeId = await _storeService.GetStoreIdByGuidAsync(raincheckDto.StoreId);
        if (storeId != null)
        {
            var productId = await _productService.GetProductIdAsync(raincheckDto.ProductId);
            if (productId != null)
            {
                raincheck.StoreId = (int)storeId;
                raincheck.ProductId = (int)productId;
            }
            else
            {
                throw new InvalidOperationException($"Product with Guid {raincheckDto.StoreId} not found.");
            }
        }
        else
        {
            throw new InvalidOperationException($"Store with Guid {raincheckDto.ProductId} not found.");
        }

        _db.Rainchecks.Add(raincheck);
        await _db.SaveChangesAsync();

        return _mapper.Map<RaincheckDto>(raincheck);
    }

    public async Task<Results<Ok, NotFound, BadRequest>> UpdateRaincheckAsync(Guid guid, RaincheckDto raincheckDto)
    {
        var raincheck = await _db.Rainchecks.FirstOrDefaultAsync(p => p.RaincheckGuid == guid);

        if (guid != raincheckDto.Id)
        {
            return TypedResults.BadRequest();
        }
    
        var storeId = await _storeService.GetStoreIdByGuidAsync(raincheckDto.Store.Id);
        if (storeId != null)
        {
            var productId = await _productService.GetProductIdAsync(raincheckDto.Product.Id);
            if (productId != null)
            {
                var rowsAffected = await _db.Rainchecks.Where(r => r.RaincheckGuid == guid)
                        .AsNoTracking()
                        .ExecuteUpdateAsync(updates =>
                                                updates.SetProperty(p => p.Name, raincheckDto.Name)
                                                       .SetProperty(p => p.Count, raincheckDto.Count)
                                                       .SetProperty(p => p.SalePrice, raincheckDto.SalePrice)
                                                       .SetProperty(p => p.ProductId, productId)
                                                       .SetProperty(p => p.StoreId, storeId));

                return rowsAffected == 0 ? TypedResults.NotFound() : TypedResults.Ok();
            }
            else
            {
                throw new InvalidOperationException($"Product with Guid {raincheckDto.Product.Id} not found.");
            }
        }
        else
        {
            throw new InvalidOperationException($"Store with Guid {raincheckDto.Store.Id} not found.");
        }

        //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        //.ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
        //.ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
    }
}