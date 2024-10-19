namespace WebAndroid.Models
{
    public class CategoryCreationModel
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
