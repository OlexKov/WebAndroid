using AutoMapper;
using WebAndroid.Data.Entities;
using WebAndroid.Models.Product;

namespace WebAndroid.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductItemViewModel>()
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages
                    .Select(p => p.Image).ToArray()));

            CreateMap<ProductCreateViewModel, ProductEntity>();

            CreateMap<ProductEditViewModel, ProductEntity>()
                .ForMember(x => x.ProductImages, opt => opt.Ignore());

        }
    }
}
