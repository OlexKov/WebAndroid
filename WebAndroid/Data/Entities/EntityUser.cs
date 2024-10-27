using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAndroid.Data.Entities
{
    public class EntityUser : IdentityUser<int>
    {
        [Required, StringLength(255)]
        public string? FirstName { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string? LastName { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string? Photo { get; set; } = string.Empty;
    }
}
