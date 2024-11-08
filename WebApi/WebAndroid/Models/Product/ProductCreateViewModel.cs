using Microsoft.AspNetCore.Mvc;

namespace WebAndroid.Models.Product
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [BindProperty(Name = "images[]")]
        public ICollection<IFormFile> Images { get; set; } = new HashSet<IFormFile>();
    }
}
