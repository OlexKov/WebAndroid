using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAndroid.Data.Entities;
using WebAndroid.DTO;
using WebAndroid.Interfaces;
using WebAndroid.Models;

namespace WebAndroid.Services
{
    public class CategoryService(IRepository<Category> repository,IMapper mapper,IImageService imageService) : ICategoryService
    {
        private readonly IRepository<Category> repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IImageService imageService = imageService;

        public async Task<CategoryDto> CreateUpdateAsync(CategoryCreationModel model)
        {
            Category category;
            string? image = model.ImageFile is not null ? await imageService.SaveImageAsync(model.ImageFile) : null;
            if (model.Id == 0)
            {
                category = mapper.Map<Category>(model);
                category.Image = image;
                await repository.AddAsync(category);
            }
            else
            {
                category = await repository.GetByIDAsync(model.Id)
                    ?? throw new HttpException("Invalid category id", HttpStatusCode.BadRequest);
                category.Name = model.Name;
                category.Description = model.Description;
                if (image is not null) 
                {
                    if(category.Image is not null)
                       imageService.DeleteImage(category.Image);
                    category.Image = image;
                }
            }
             
            await repository.SaveAsync();
            return mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category =  await repository.GetByIDAsync(id) ?? throw new HttpException("Invalid category id", HttpStatusCode.BadRequest);
            repository.Delete(category);
            await repository.SaveAsync();
            imageService.DeleteImageIfExists(category.Image);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync() =>  mapper.Map<IEnumerable<CategoryDto>>(await repository.GetAll().ToArrayAsync());

        public async Task<CategoryDto> GetByIdAsync(int id) => mapper.Map<CategoryDto>(await repository.GetByIDAsync(id));
        
    }
}
