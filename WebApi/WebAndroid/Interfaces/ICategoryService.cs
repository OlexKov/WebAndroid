using WebAndroid.DTO;
using WebAndroid.Models;

namespace WebAndroid.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> CreateUpdateAsync(CategoryCreationModel model);
        Task DeleteAsync(int id);
        Task<CategoryDto> GetByIdAsync(int id);


    }
}
