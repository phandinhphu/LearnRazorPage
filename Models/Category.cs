using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tên danh mục không được bỏ trống")]
        public required string Name { get; set; }
        
        [StringLength(100)]
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
