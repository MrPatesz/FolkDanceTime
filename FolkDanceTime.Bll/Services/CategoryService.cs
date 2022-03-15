using FolkDanceTime.Dal.Data;
using FolkDanceTime.Dal.Entities;

namespace FolkDanceTime.Bll.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Category> GetCategories()
        {
            return dbContext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);

            return category;
        }

        public async Task<Category> AddCategory(Category category)
        {
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategoryById(int id)
        {
            var toRemove = dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (toRemove != null)
            {
                dbContext.Categories.Remove(toRemove);
                await dbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Category> EditCategory(Category category)
        {
            var toEdit = dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (toEdit == null)
            {
                return null;
            }

            toEdit.Name = category.Name;

            dbContext.Categories.Update(toEdit);
            await dbContext.SaveChangesAsync();

            return toEdit;
        }
    }
}
