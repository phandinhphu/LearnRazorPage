using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name = "")
        {
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
