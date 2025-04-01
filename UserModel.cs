using System.ComponentModel.DataAnnotations;

namespace UserService
{
    public class User
    {
        [Key]
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public string? Gender { get; set; }
        public string? Number { get; set; }
        public string? Role { get; set; }
    }
}
