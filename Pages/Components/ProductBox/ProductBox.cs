using Microsoft.AspNetCore.Mvc;
using WebAppTest.Services;

namespace WebAppTest.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        private readonly ProductServices _productServices;
        public List<Models.Product> Products { get; set; }

        public ProductBox(ProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool sort = true)
        {
            await Task.Delay(1000); // Đợi 1 giây

            if (sort)
            {
                Products = _productServices.GetProducts().OrderBy(p => p.Price).ToList();
            }
            else
            {
                Products = _productServices.GetProducts().OrderByDescending(p => p.Price).ToList();
            }
            return View(Products);
        }
    }
}
