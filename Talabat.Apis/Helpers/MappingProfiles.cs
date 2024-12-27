using AutoMapper;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;

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


        }
    }
}
