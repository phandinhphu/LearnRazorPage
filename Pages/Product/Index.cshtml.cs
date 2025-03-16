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
        private readonly WebAppTest.Data.ApplicationDbContext _context;
        private readonly IProductService _productService;
        [BindProperty(SupportsGet = true)]
        public string QueryString { get; set; } = string.Empty;

        public IndexModel(WebAppTest.Data.ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = (await _productService.GetProductsAsync(QueryString)).ToList();
        }

        public IActionResult OnPostAction(int[] productsId, string action)
        {
            //if (action == "clear")
            //{
            //    _productServices.ClearProducts(productsId);
            //}
            //else if (action == "restore")
            //{
            //    _productServices.RestoreProducts();
            //}

            Console.WriteLine(action);
            productsId.ToList().ForEach(p => Console.WriteLine(p));

            return new RedirectResult("/Product/Index");
        }
    }
}
