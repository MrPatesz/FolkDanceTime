using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class ItemService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PictureService _pictureService;

        public ItemService(ApplicationDbContext dbContext, IMapper mapper, PictureService pictureService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _pictureService = pictureService;
        }

        public async Task<List<ItemDto>> GetItemsAsync()
        {
            return await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemDto>> GetMyItemsAsync(string userId)
        {
            return await _dbContext.Items
                .Where(i => i.OwnerUserId == userId)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemDto> GetItemAsync(int id)
        {
            return await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .SingleAsync(i => i.Id == id);
        }

        public async Task<ItemDto> AddItemAsync(ItemDto itemDto, int categoryId, string userId)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                CategoryId = categoryId,
                OwnerUserId = userId,
                PictureFilename = itemDto.PictureFilename,
            };

            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            var propertyValues = itemDto.Properties.Select(p =>
                new PropertyValue
                {
                    Value = p.Value,
                    ItemId = item.Id,
                    PropertyId = p.PropertyId,
                }
            );

            var category = await _dbContext.Categories
                .Include(c => c.Properties)
                .SingleAsync(c => c.Id == categoryId);

            category.Properties.ForEach(p =>
            {
                var isAlreadyThere = propertyValues.Any(pv => pv.PropertyId == p.Id);
                if (!isAlreadyThere)
                {
                    propertyValues.Append(new PropertyValue
                    {
                        Value = "",
                        ItemId = item.Id,
                        PropertyId = p.Id,
                    });
                }
            });

            await _dbContext.PropertyValues.AddRangeAsync(propertyValues);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemDto>(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _dbContext.Items.SingleAsync(i => i.Id == id);

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ItemDto> EditItemAsync(ItemDto itemDto)
        {
            var item = await _dbContext.Items
                .Include(i => i.PropertyValues)
                .ThenInclude(i => i.Property)
                .SingleAsync(c => c.Id == itemDto.Id);

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.PropertyValues.ForEach(pv =>
            {
                pv.Value = itemDto.Properties.Single(p => p.PropertyValueId == pv.Id).Value;
            });

            if (item.PictureFilename != null && item.PictureFilename != itemDto.PictureFilename)
            {
                _pictureService.DeletePicture(item.PictureFilename);
            }
            item.PictureFilename = itemDto.PictureFilename;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemDto>(item);
        }
    }
}
