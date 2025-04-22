using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTest.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public ICollection<Product>? Products { get; set; }
    }
}
