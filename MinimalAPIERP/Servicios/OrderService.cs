using ERP.Data;
using ERP;
using AutoMapper;
using BlazorStoreApp.Infraestructura.Dtos;
using MinimalAPIERP.Servicios;

public class OrderService: IOrderService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _db;
    private readonly IProductService _productService;
       
    public OrderService(IMapper mapper, AppDbContext db, IProductService productService)
    {
        _mapper = mapper;
        _db = db;
        _productService = productService;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        order.OrderDetails =_mapper.Map<List<OrderDetail>>(orderDto.OrderDetails);

        foreach (OrderDetail detail in order.OrderDetails)
        {
            var productId = await _productService.GetProductIdAsync(detail.Product.ProductGuid);

            if (productId != null)
            {
                detail.ProductId = (int)productId;
                detail.Product = null;
            }
            else
            {
                throw new InvalidOperationException($"Product with Guid {detail.Product.ProductGuid} not found.");
            }
        }

        order.Username = "dloeches";
        order.OrderDate = DateTime.Now;

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }
}