using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAndroid.Data.Entities
{
    [Table("tblCategories")]
    public class Category
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [StringLength(4000)]
        public string? Description { get; set; } = string.Empty;
    }
}
