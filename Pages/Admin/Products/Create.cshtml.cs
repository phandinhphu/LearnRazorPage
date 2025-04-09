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

namespace WebAppTest.Admin.Pages_Product
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
        public async Task<IActionResult> OnPostAsync(List<IFormFile>? imgFiles)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Lỗi tại {entry.Key}: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            List<string> imgs = new List<string>();
            if (imgFiles != null && imgFiles.Count > 0)
            {
                foreach (var imgFile in imgFiles)
                {
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

                        imgs.Add("uploads/products/" + fileName);
                    }
                }
            }

            _productServices.SetJsonSerializeProductImage(Product, imgs);

            await _productServices.AddProductAsync(Product);

            return RedirectToPage("./Index");
        }
    }
}
