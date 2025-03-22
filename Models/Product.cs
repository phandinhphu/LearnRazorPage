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
        [StringLength(100)]
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public required string Name { get; set; }
        [StringLength(500)]
        public required string Description { get; set; }
        [StringLength(500)]
        public string? Image { get; set; }
        [Required(ErrorMessage = "Giá không được bỏ trống")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
