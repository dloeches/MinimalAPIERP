using AutoMapper;
using ERP;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Infraestructure.Automapper 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StoreGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<StoreCreateDto, Store>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

           CreateMap<ProductDto, Product>()
               .ForMember(dest => dest.ProductGuid, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
               .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.ProductGuid, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
           .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Raincheck, RaincheckDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RaincheckGuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<RaincheckCreateDto, Raincheck>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.RaincheckGuid, opt => opt.Ignore())
                .ForMember(dest => dest.StoreId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Store, opt => opt.Ignore());
        
        CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CartItemGuid))
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Order, OrderDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderGuid))
              .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
              .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
              .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
              .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
              .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
              .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<OrderDetail, OrderDetailDto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderDetailGuid))
             .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
             .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
             .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
             .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));
        }
    }
}