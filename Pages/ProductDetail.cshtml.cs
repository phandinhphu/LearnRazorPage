using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        public Product Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public ProductDetailModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGet()
        {
            Product = await _productService.GetProductByIdAsync(Id);
            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
    }
}
