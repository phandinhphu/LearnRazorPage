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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IWebHostEnvironment webHostEnvironment,IProductService productServices)
        {
            _webHostEnvironment = webHostEnvironment;
            _productServices = productServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? imgFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (imgFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgFile.FileName);
                var uploadForder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");

                if (!Directory.Exists(uploadForder))
                {
                    Directory.CreateDirectory(uploadForder);
                }

                var uploadPath = Path.Combine(uploadForder, fileName);

                using (var fs = new FileStream(uploadPath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(fs);
                }

                Product.Image = "uploads/products/" + fileName;
            }

            await _productServices.AddProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
