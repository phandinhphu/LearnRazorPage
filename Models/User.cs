using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    public enum UserRole
    {
        Admin,
        Customer
    }

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "Độ dài tối thiểu là 25")]
        [Required(ErrorMessage = "Tên hiển thị không được bỏ trống")]
        public required string DisplayName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public required string Email { get; set; }

        [StringLength(maximumLength: 6, ErrorMessage = "Độ dài tối thiểu là 6")]
        [Required(ErrorMessage = "Password không được bỏ trống")]
        public required string Password { get; set; }

        public UserRole? Role { get; set; }
    }
}
