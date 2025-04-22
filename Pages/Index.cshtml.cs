using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppTest.Helpers;
using WebAppTest.Models;
using WebAppTest.Services;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IProductService _productService;

    public PaginatedList<Product> Products { get; private set; }
    [BindProperty(SupportsGet = true)]
    public int PageIndex { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public int CategoryID { get; set; }
    [BindProperty(SupportsGet = true)]
    public string QuerySearch { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IProductService productServices)
    {
        _logger = logger;
        _productService = productServices;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Products = await _productService.GetProductsAsync(CategoryID, QuerySearch, PageIndex, 20);

        return Page();
    }
}
