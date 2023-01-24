using AutoMapper;
using Warehouse.ProductService.API.ViewModels;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.API.Mapper
{
    public class ModelViewModelProfile : Profile
    {
        public ModelViewModelProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<PostCategoryViewModel, Category>();
            CreateMap<UpdateCategoryViewModel, Category>();

            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<PostProductViewModel, Product>();
            CreateMap<UpdateProductViewModel, Product>();
        }
    }
}
