using System.ComponentModel.DataAnnotations;

namespace WebAndroid.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
