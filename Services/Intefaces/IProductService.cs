using WebAppTest.Helpers;
using WebAppTest.Models;

namespace WebAppTest.Services.Intefaces
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> GetProductsAsync(int? categoryID, string name = "", int pageIndex = 1, int pageSize = 20);
        Task<IEnumerable<Product>> GetDeletedProductsAsync(string name = "");
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int id = 0);
        Task<Product> GetProductByIdAsync(int id);
        Task<int> GetDeletedProductsCountAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task UpdateRangeProductsAsync(IEnumerable<Product> products);
        Task SortDeleteProductAsync(int[] id);
        Task RestoreProductAsync(int[] id);
        Task DestroyProductAsync(int[] id);
        void SetJsonSerializeProductImage(Product product, List<string> jsonImg);
        List<string> GetJsonDeserializeProductImage(Product product);
    }
}
