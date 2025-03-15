using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Models;
using WebAppTest.Services;

namespace WebAppTest.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ProductServices _productServices;

    public List<Product> Products { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, ProductServices productServices)
    {
        _logger = logger;
        _productServices = productServices;
    }

    public void OnGet()
    {
        Products = _productServices.GetProducts().ToList();
    }
}
