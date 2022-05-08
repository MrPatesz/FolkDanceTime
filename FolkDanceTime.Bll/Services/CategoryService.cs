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
            // TODO: throw exception ha null, MVC action filter vagy middleware elkapja ezt
            // problem details nevű middleware, config: milyen kivételekre milyen hibakódot adjon

            return await _dbContext.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .SingleAsync(c => c.Id == id);
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.SingleAsync(c => c.Id == id);

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CategoryDto> EditCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _dbContext.Categories
                .Include(c => c.Items)
                .Include(c => c.Properties)
                .ThenInclude(p => p.PropertyValues)
                .SingleAsync(c => c.Id == categoryDto.Id);

            // editing existing properties
            category.Name = categoryDto.Name;
            category.Properties.ForEach(property =>
            {
                if (categoryDto.Properties.Any(p => p.Id == property.Id))
                {
                    property.Name = categoryDto.Properties.Single(p => p.Id == property.Id).Name;
                }
            });

            // deleting removed properties and their values
            var propertiesToDelete = category.Properties.Where(property => !categoryDto.Properties.Any(p => p.Id == property.Id));
            foreach (var property in propertiesToDelete)
            {
                _dbContext.PropertyValues.RemoveRange(property.PropertyValues);
            }
            _dbContext.Properties.RemoveRange(propertiesToDelete);

            // adding new properties with empty property values
            var propertiesToAdd = categoryDto.Properties
                .Where(p => p.Id == 0)
                .Select(p =>
                    new Property
                    {
                        Name = p.Name,
                        CategoryId = categoryDto.Id,
                        PropertyValues = category.Items
                            .Select(i => new PropertyValue
                            {
                                Value = "",
                                ItemId = i.Id,
                            })
                            .ToList(),
                    }
                );
            await _dbContext.Properties.AddRangeAsync(propertiesToAdd);

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
