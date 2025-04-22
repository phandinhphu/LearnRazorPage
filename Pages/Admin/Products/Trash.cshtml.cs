using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Admin.Pages_Product
{
    [Authorize(Policy = "Admin")]
    public class TrashModel : PageModel
    {
        private readonly IProductService _productService;
        [BindProperty(SupportsGet = true)]
        public string QueryString { get; set; } = string.Empty;

        public TrashModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = (await _productService.GetDeletedProductsAsync(QueryString)).ToList();
        }

        public async Task<IActionResult> OnPostActionAsync(int[] productsId, string action)
        {
            if (action == "destroy")
            {
                await _productService.DestroyProductAsync(productsId);
            }
            else if (action == "restore")
            {
                await _productService.RestoreProductAsync(productsId);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDestroyAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            await _productService.DestroyProductAsync(new[] { id });
            return RedirectToPage("./Trash");
        }

        public async Task<IActionResult> OnPostRestoreAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            await _productService.RestoreProductAsync(new[] { id });
            return RedirectToPage("./Trash");
        }
    }
}
