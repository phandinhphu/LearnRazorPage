using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages_Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public int DeletedProductsCount { get; set; }
        [BindProperty(SupportsGet = true)]
        public string QueryString { get; set; } = string.Empty;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = (await _productService.GetProductsAsync(QueryString)).ToList();
            DeletedProductsCount = await _productService.GetDeletedProductsCountAsync();
        }

        public async Task<IActionResult> OnPostActionAsync(int[] productsId, string action)
        {
            if (action == "clear")
            {
                await _productService.SortDeleteProductAsync(productsId);
            }
            else if (action == "restore")
            {
                //await _productService.RestoreProductAsync();
            }

            return RedirectToPage();
        }
    }
}
