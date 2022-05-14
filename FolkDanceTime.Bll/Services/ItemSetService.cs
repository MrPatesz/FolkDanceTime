using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class ItemSetService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemSetService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemSetDto>> GetItemSetsAsync()
        {
            return await _dbContext.ItemSets
                .ProjectTo<ItemSetDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemSetDto>> GetMyItemSetsAsync(string userId)
        {
            return await _dbContext.ItemSets
                .Where(i => i.OwnerUserId == userId)
                .ProjectTo<ItemSetDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemSetDto> GetItemSetAsync(int id)
        {
            return await _dbContext.ItemSets
                .ProjectTo<ItemSetDto>(_mapper.ConfigurationProvider)
                .SingleAsync(i => i.Id == id);
        }

        public async Task<ItemSetDto> AddItemSetAsync(ItemSetDto itemSetDto, string userId)
        {
            var itemSet = new ItemSet
            {
                Name = itemSetDto.Name,
                OwnerUserId = userId,
            };
            await _dbContext.ItemSets.AddAsync(itemSet);
            await _dbContext.SaveChangesAsync();

            var allItems = await _dbContext.Items.ToListAsync();
            var results = allItems.Where(item => itemSetDto.Items.Any(i => i.Id == item.Id)).ToList();

            foreach(var item in results)
            {
                item.ItemSetId = itemSet.Id;
            }

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemSetDto>(itemSet);
        }

        public async Task<ItemSetDto> EditItemSetAsync(ItemSetDto itemSetDto)
        {
            var itemSet = await _dbContext.ItemSets
                .Include(i => i.Items)
                .SingleAsync(c => c.Id == itemSetDto.Id);

            itemSet.Name = itemSetDto.Name;

            var itemsToAdd = itemSetDto.Items
                .Where(i => !itemSet.Items.Any(item => item.Id == i.Id))
                .ToList();

            await _dbContext.Items.ForEachAsync(i =>
            {
                if(itemsToAdd.Any(item => item.Id == i.Id))
                {
                    i.ItemSetId = itemSet.Id;
                }
            });

            itemSet.Items.RemoveAll(i => !itemSetDto.Items.Any(item => item.Id == i.Id));

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemSetDto>(itemSet);
        }

        public async Task DeleteItemSetAsync(int id)
        {
            var itemSet = await _dbContext.ItemSets
                .SingleAsync(i => i.Id == id);

            _dbContext.ItemSets.Remove(itemSet);
            await _dbContext.SaveChangesAsync();
        }
    }
}
