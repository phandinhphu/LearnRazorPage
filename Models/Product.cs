using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace WebAppTest.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string? UserId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public required string Name { get; set; }

        [StringLength(500)]
        public required string Description { get; set; }

        [StringLength(500)]
        public string? Image { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        [Required(ErrorMessage = "Giá không được bỏ trống")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        [Required(ErrorMessage = "Số lượng không được bỏ trống")]
        public int Stock { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Category? Category { get; set; }
        public User? User { get; set; }
    }
}
