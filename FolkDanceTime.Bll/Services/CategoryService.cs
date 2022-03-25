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
            var category = await _dbContext.Categories.FindAsync(id);

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
            var category = await _dbContext.Categories.FindAsync(categoryDto.Id);

            if (category == null)
            {
                throw new Exception();
            }

            category.Name = categoryDto.Name;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
