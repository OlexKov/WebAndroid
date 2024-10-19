using AutoMapper;
using WebAndroid.Data.Entities;
using WebAndroid.Interfaces;
using WebAndroid.Models;

namespace WebAndroid.Mappers.Actions
{
    public class CategoryImageAction(IImageService imageService) : IMappingAction<CategoryCreationModel, Category>
    {
        public void Process(CategoryCreationModel source, Category destination, ResolutionContext context)
        {
            if (source.ImageFile is not null)
            {
                if(destination.Image is not null)
                    imageService.DeleteImage(destination.Image);
                destination.Image =  imageService.SaveImageAsync(source.ImageFile).Result;
            }
            else 
            {
                destination.Image = destination.Image;
            }
        }
    }
}
