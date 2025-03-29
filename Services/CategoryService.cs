using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;
using WebAppTest.Services.Intefaces;

namespace WebAppTest.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DestroyCategoryAsync(int[] id)
        {
            var categories = await _context.Categories
                                .Where(c => id.Contains(c.Id))
                                .ToListAsync();

            _context.Categories.RemoveRange(categories);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string name = "")
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public Task<IEnumerable<Category>> GetDeletedCategoriesAsync(string name = "")
        {
            throw new NotImplementedException();
        }

        public Task<int> GetDeletedCategoriesCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task RestoreCategoryAsync(int[] id)
        {
            throw new NotImplementedException();
        }

        public Task SortDeleteCategoryAsync(int[] id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeCategoriesAsync(IEnumerable<Category> categories)
        {
            _context.Categories.UpdateRange(categories);
            await _context.SaveChangesAsync();
        }
    }
}
