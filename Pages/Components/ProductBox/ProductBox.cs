using Microsoft.AspNetCore.Mvc;
using WebAppTest.Helpers;
using WebAppTest.Models;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        private readonly IProductService _productService;
        public ProductBox(IProductService productServices)
        {
            _productService = productServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(PaginatedList<Product> Products, bool sort = true)
        {
            if (sort)
            {
                //Products = Products.OrderBy(p => p.Price).ToList();
            }
            else
            {
                //Products = Products.OrderByDescending(p => p.Price).ToList();
            }
            return View(Products);
        }
    }
}
