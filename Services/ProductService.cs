using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DestroyProductAsync(int[] id)
        {
            var products = await _context.Products
                                .IgnoreQueryFilters()
                                .Where(p => id.Contains(p.Id))
                                .ToListAsync();

            products.ForEach(product =>
            {
                if (product.Image != null)
                {
                    var imgs = JsonSerializer.Deserialize<List<string>>(product.Image);
                    if (imgs == null) return;
                    foreach (var img in imgs)
                    {
                        var imgPath = Path.Combine(_webHostEnvironment.WebRootPath, img);
                        if (File.Exists(imgPath))
                        {
                            File.Delete(imgPath);
                        }
                    }
                }
            });

            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetDeletedProductsAsync(string name = "")
        {
            return await _context.Products
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted && p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<int> GetDeletedProductsCountAsync()
        {
            return await _context.Products
                .IgnoreQueryFilters()
                .CountAsync(p => p.IsDeleted);
        }

        public List<string> GetJsonDeserializeProductImage(Product product)
        {
            return JsonSerializer.Deserialize<List<string>>(product.Image);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Products.ToListAsync();
            }

            var products = from p in _context.Products
                           where p.Name.Contains(name)
                           select p;

            return await products.ToListAsync();
        }

        public async Task RestoreProductAsync(int[] id)
        {
            var products = await _context.Products
                                .IgnoreQueryFilters()
                                .Where(p => id.Contains(p.Id))
                                .ToListAsync();

            products.ForEach(p => p.IsDeleted = false);

            await UpdateRangeProductsAsync(products);
        }

        public void SetJsonSerializeProductImage(Product product, List<string> jsonImg)
        {
            product.Image = JsonSerializer.Serialize(jsonImg);
        }

        public async Task SortDeleteProductAsync(int[] id)
        {
            foreach (var i in id)
            {
                var product = await GetProductByIdAsync(i);
                product.IsDeleted = true;
                await UpdateProductAsync(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeProductsAsync(IEnumerable<Product> products)
        {
            _context.Products.UpdateRange(products);
            await _context.SaveChangesAsync();
        }
    }
}
