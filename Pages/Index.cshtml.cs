using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductService _productService;

    public IList<Product> Products { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, IProductService productServices)
    {
        _logger = logger;
        _productService = productServices;
    }

    public async Task OnGetAsync()
    {
        Products = (await _productService.GetProductsAsync()).ToList();
    }
}
