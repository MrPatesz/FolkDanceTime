using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await _dbContext.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var category = await _dbContext.Categories
                .Include(c => c.Properties)
                .FirstAsync(c => c.Id == id);

            // TODO: throw exception ha null, MVC action filter vagy middleware elkapja ezt
            // problem details nevű middleware, config: milyen kivételekre milyen hibakódot adjon

            if (category == null)
            {
                throw new Exception();
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
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

        public async Task<CategoryDto> EditCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _dbContext.Categories.Include(c => c.Properties).FirstAsync(c => c.Id == categoryDto.Id);

            if (category == null)
            {
                throw new Exception();
            }

            category.Name = categoryDto.Name;
            category.Properties.ForEach(property =>
            {
                if(categoryDto.Properties.Any(p => p.Id == property.Id))
                {
                    property.Name = categoryDto.Properties.First(p => p.Id == property.Id).Name;
                }
            });

            var propertiesToAdd = categoryDto.Properties.Where(p => p.Id == 0).ToList();

            propertiesToAdd.ForEach(p => _dbContext.Properties.Add(new Property{
                Name = p.Name,
                CategoryId = categoryDto.Id,
            }));

            var propertiesToDelete = category.Properties.Where(property => !categoryDto.Properties.Any(p => p.Id == property.Id));

            _dbContext.Properties.RemoveRange(propertiesToDelete);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
