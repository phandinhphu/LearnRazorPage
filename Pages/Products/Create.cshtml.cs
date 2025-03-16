using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages_Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productServices;

        public CreateModel(IProductService productServices)
        {
            _productServices = productServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productServices.AddProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
