using ERP.Data;
using ERP;
using AutoMapper;
using BlazorStoreApp.Infraestructura.Dtos;
using MinimalAPIERP.Servicios;
using Microsoft.EntityFrameworkCore;

public class CartItemService: ICartItemService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;
    private readonly IProductService _productService;

    public CartItemService(IMapper mapper, AppDbContext db, IProductService productService)
    {
        _mapper = mapper;
        _db = db;
        _productService = productService;
    }

    public async Task<CartItemDto> CreateCartItemAsync(CartItemDto cartItemDto)
    {
        var cartItem = _mapper.Map<CartItem>(cartItemDto);

        var productId = await _productService.GetProductIdAsync(cartItemDto.Product.Id);
        if (productId != null)
        {
            cartItem.ProductId = (int)productId;
        }
        else
        {
            throw new InvalidOperationException($"Product with Guid {cartItemDto.Id} not found.");
        }

        cartItem.DateCreated = DateTime.Now;

        _db.CartItems.Add(cartItem);
        await _db.SaveChangesAsync();

        return _mapper.Map<CartItemDto>(cartItem);
    }
}