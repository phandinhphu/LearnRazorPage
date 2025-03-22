using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages_Product
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productservice;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IWebHostEnvironment webHostEnvironment, IProductService productService)
        {
            _webHostEnvironment = webHostEnvironment;
            _productservice = productService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productservice.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
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

            try
            {
                Console.WriteLine(Product.Image);
                Product product = await _productservice.GetProductByIdAsync(Product.Id);
                Console.WriteLine(product);

                List<string> imgs = new List<string>();
                List<string> existImgs = _productservice.GetJsonDeserializeProductImage(product);

                if (Images != null && Images.Count > 0)
                {

                    foreach (var imgFile in Images)
                    {
                        string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");

                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imgFile.FileName);
                        string uploadPath = Path.Combine(uploadFolder, uniqueFileName);
                        using (var fs = new FileStream(uploadPath, FileMode.Create))
                        {
                            await imgFile.CopyToAsync(fs);
                        }

                        imgs.Add("uploads/products/" + uniqueFileName);
                    }
                }
                imgs.AddRange(existImgs);
                _productservice.SetJsonSerializeProductImage(Product, imgs);

                await _productservice.UpdateProductAsync(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _productservice.GetProductByIdAsync(id).Result != null;
        }
    }
}
