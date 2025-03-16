using WebAppTest.Models;

namespace WebAppTest.Services.Intefaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string name = "");
        Task<IEnumerable<Product>> GetDeletedProductsAsync(string name = "");
        Task<Product> GetProductByIdAsync(int id);
        Task<int> GetDeletedProductsCountAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task UpdateRangeProductsAsync(IEnumerable<Product> products);
        Task SortDeleteProductAsync(int[] id);
        Task RestoreProductAsync(int[] id);
        Task DestroyProductAsync(int[] id);
    }
}
