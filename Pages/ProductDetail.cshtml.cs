using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Extensions;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly SignInManager<User> _signInManager;

        public Product Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public ProductDetailModel(IProductService productService, SignInManager<User> signInManager)
        {
            _productService = productService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            Product = await _productService.GetProductByIdAsync(Id);
            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCart(int productId, int quantity)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToPage(
                    "/Account/Login", 
                    new { area = "Identity", returnUrl = Url.Page("/ProductDetail", new { id = productId }) }
                );
            }

            Product = await _productService.GetProductByIdAsync(productId);
            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem {
                    ProductId = Product.Id,
                    CategoryId = Product.CategoryId,
                    ProductName = Product.Name,
                    ProductImage = Product.Image,
                    CategoryName = Product.Category.Name,
                    ProductPrice = Product.Price,
                    Quantity = quantity 
                });
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToPage("/ProductDetail", new { id = productId });
        }
    }
}