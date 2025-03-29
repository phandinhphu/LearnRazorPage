using WebAppTest.Models;

namespace WebAppTest.Services.Intefaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(string name = "");
        Task<IEnumerable<Category>> GetDeletedCategoriesAsync(string name = "");
        Task<Category> GetCategoryByIdAsync(int id);
        Task<int> GetDeletedCategoriesCountAsync();
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task UpdateRangeCategoriesAsync(IEnumerable<Category> categories);
        Task SortDeleteCategoryAsync(int[] id);
        Task RestoreCategoryAsync(int[] id);
        Task DestroyCategoryAsync(int[] id);
    }
}
