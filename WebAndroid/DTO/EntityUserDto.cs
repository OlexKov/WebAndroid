namespace WebAndroid.DTO
{
    public class EntityUserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
