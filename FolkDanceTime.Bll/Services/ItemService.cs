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

        public ItemService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> GetItemsAsync()
        {
            return await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemDto> GetItemAsync(int id)
        {
            var item = await _dbContext.Items.FindAsync(id); // TODO: maybe need to include some properties

            if (item == null)
            {
                throw new Exception();
            }

            return _mapper.Map<ItemDto>(item);
        }

        public async Task<ItemDto> AddItemAsync(ItemDto itemDto, int categoryId, string userId)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                CategoryId = categoryId,
                OwnerUserId = userId,
            };

            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemDto>(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);

            if (item == null)
            {
                throw new Exception();
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ItemDto> EditItemAsync(ItemDto itemDto)
        {
            var item = await _dbContext.Items.FindAsync(itemDto.Id);

            if (item == null)
            {
                throw new Exception();
            }

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            // TODO: maybe set ownerId too or create a new function for that

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemDto>(item);
        }
    }
}
