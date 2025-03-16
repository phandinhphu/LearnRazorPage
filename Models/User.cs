using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string? DisplayName { get; set; }
        [StringLength(100)]
        [Required]
        public string? Email { get; set; }
        [StringLength(100)]
        [Required]
        public string? Password { get; set; }
    }
}
