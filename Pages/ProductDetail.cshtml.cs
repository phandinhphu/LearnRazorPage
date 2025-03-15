using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppTest.Pages
{
    public class ProductDetailModel : PageModel
    {
        public int BookId { get; set; }

        public void OnGet(int id)
        {
            BookId = id;
        }
    }
}
