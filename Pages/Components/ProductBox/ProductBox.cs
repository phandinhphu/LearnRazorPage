using Microsoft.AspNetCore.Mvc;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        private readonly IProductService _productService;
        public List<Models.Product> Products { get; set; }

        public ProductBox(IProductService productServices)
        {
            _productService = productServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool sort = true)
        {
            await Task.Delay(1000); // Đợi 1 giây

            var products = await _productService.GetProductsAsync();
            if (sort)
            {
                Products = products.OrderBy(p => p.Price).ToList();
            }
            else
            {
                Products = products.OrderByDescending(p => p.Price).ToList();
            }
            return View(Products);
        }
    }
}
