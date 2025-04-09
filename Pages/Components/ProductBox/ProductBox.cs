using Microsoft.AspNetCore.Mvc;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        private readonly IProductService _productService;
        public List<Models.Product> Products { get; set; } = new List<Models.Product>();

        public ProductBox(IProductService productServices)
        {
            _productService = productServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CategoryID, string QuerySearch = "", bool sort = true)
        {
            var products = CategoryID > 0 ?
                            await _productService.GetProductsByCategoryAsync(CategoryID) :
                            await _productService.GetProductsAsync();
            if (sort)
            {
                products = products.OrderBy(p => p.Price).ToList();
            }
            else
            {
                products = products.OrderByDescending(p => p.Price).ToList();
            }

            if (!string.IsNullOrEmpty(QuerySearch))
            {
                Products = products.Where(p => p.Name.ToLower().Contains(QuerySearch.ToLower())).ToList();
            }
            else
            {
                Products = products.ToList();
            }
            return View(Products);
        }
    }
}
