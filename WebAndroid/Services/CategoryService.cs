using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAndroid.Data.Entities;
using WebAndroid.DTO;
using WebAndroid.Interfaces;
using WebAndroid.Models;

namespace WebAndroid.Services
{
    public class CategoryService(IRepository<Category> repository,IMapper mapper) : ICategoryService
    {
        private readonly IRepository<Category> repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<CategoryDto> CreateUpdateAsync(CategoryCreationModel model)
        {
            var category = mapper.Map<Category>(model);
            if (model.Id == 0)
            {
                await repository.AddAsync(category);
            }
            else if ((await repository.GetByIDAsync(model.Id)) is not null)
            {
                repository.Update(category);
            }
            else throw new HttpException("Invalid category id",HttpStatusCode.BadRequest);
            await repository.SaveAsync();
            return mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category =  await repository.GetByIDAsync(id) ?? throw new HttpException("Invalid category id", HttpStatusCode.BadRequest);
            repository.Delete(category);
            await repository.SaveAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync() =>  mapper.Map<IEnumerable<CategoryDto>>(await repository.GetAll().ToArrayAsync());

        public async Task<CategoryDto> GetByIdAsync(int id) => mapper.Map<CategoryDto>(await repository.GetByIDAsync(id));
        
    }
}
