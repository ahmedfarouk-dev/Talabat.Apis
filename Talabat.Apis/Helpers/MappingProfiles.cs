using AutoMapper;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Apis.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles(IConfiguration configuration)
        {
            CreateMap<Product, ProductToReturn>()
                .ForMember(dto => dto.BrandName, mod => mod.MapFrom(m => m.ProductBrand.Name))
                .ForMember(dto => dto.CategoryName, mod => mod.MapFrom(m => m.productCategory.Name))
                .ForMember(dto => dto.PictureUrl, mod => mod.MapFrom(m => $"{configuration["BaseApiUrl"]}/{m.PictureUrl}"));



            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();


            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dt => dt.DeliveryMethodShortName, mod => mod.MapFrom(m => m.DeliveryMethod.ShortName))
                .ForMember(dt => dt.DeliveryMethodCost, mod => mod.MapFrom(m => m.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(dt => dt.ProductName, mod => mod.MapFrom(m => m.product.ProductName))
               .ForMember(dt => dt.ProductId, mod => mod.MapFrom(m => m.product.ProductId))
               .ForMember(dt => dt.PictureUrl, mod => mod.MapFrom(m => $"{configuration["BaseApiUrl"]}/{m.product.PictureUrl}"))

               ;


        }
    }
}
