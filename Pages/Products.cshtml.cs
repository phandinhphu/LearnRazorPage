using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;
using WebAppTest.Services;

namespace WebAppTest.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductServices _productServices;
        public List<Models.Product> Products { get; private set; }
        public Models.Product? product { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string QueryString { get; set; } = string.Empty;

        public ProductsModel(ProductServices productServices)
        {
            _productServices = productServices;
            Products = _productServices.GetProducts().ToList();
        }

        public void OnGet()
        {
            Products = _productServices.GetProducts(QueryString).ToList();
        }

        public IActionResult OnGetLastProduct()
        {
            ViewData["SubTitle"] = "Sản phẩm cuối";
            product = _productServices.GetLastProduct();
            if (product != null)
            {
                return Page();
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            bool rs = _productServices.DeleteProduct(id);

            if (rs)
            {
                return new RedirectResult("/products");
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public IActionResult OnPostAction(int[] productsId, string action)
        {
            if (action == "clear")
            {
                _productServices.ClearProducts(productsId);
            }
            else if (action == "restore")
            {
                _productServices.RestoreProducts();
            }

            return new RedirectResult("/products");
        }
    }
}
