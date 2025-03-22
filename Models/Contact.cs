using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Trường này không được bỏ trống")]
        public string? FirstName { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Trường này không được bỏ trống")]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Trường này không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }
    }
}
