using FolkDanceTime.Dal.Data;
using FolkDanceTime.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // TODO: DTO
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            // TODO: throw exception ha null, MVC action filter vagy middleware elkapja ezt
            // problem details nevű middleware, config: milyen kivételekre milyen hibakódot adjon

            if (category == null)
            {
                throw new Exception();
            }

            return category;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception();
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> EditCategoryAsync(Category categoryDto)
        {
            var category = await _dbContext.Categories.FindAsync(categoryDto.Id);

            if (category == null)
            {
                throw new Exception();
            }

            category.Name = categoryDto.Name;

            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}
