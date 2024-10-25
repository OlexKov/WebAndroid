using AutoMapper;
using WebAndroid.Data.Entities;
using WebAndroid.DTO;
using WebAndroid.Models;

namespace WebAndroid.Mappers
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreationModel, Category>();
                
        }
    }
}
