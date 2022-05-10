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

        public ItemSetService(ApplicationDbContext dbContext, IMapper mapper, PictureService pictureService)
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

        public async Task<ItemSetDto> AddItemSetAsync(ItemSetDto itemSetDto, string userId)
        {
            var itemSet = new ItemSet
            {
                Name = itemSetDto.Name,
                OwnerUserId = userId,
                Items = itemSetDto.Items
                        .Select(i => new Item { Id = i.Id })
                        .ToList(), // TODO maybe this won't work
            };

            await _dbContext.ItemSets.AddAsync(itemSet);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemSetDto>(itemSet);
        }

        public async Task<ItemSetDto> EditItemSetAsync(ItemSetDto itemSetDto)
        {
            var itemSet = await _dbContext.ItemSets
                .Include(i => i.Items)
                .SingleAsync(c => c.Id == itemSetDto.Id);

            itemSet.Name = itemSetDto.Name;
            itemSet.Items = itemSetDto.Items.Select(i => new Item { Id = i.Id }).ToList();

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ItemSetDto>(itemSet);
        }
        // add items to it (not in other itemSet and user has the item?)
        // remove items from it

        public async Task DeleteItemSetAsync(int id)
        {
            var itemSet = await _dbContext.ItemSets.SingleAsync(i => i.Id == id);

            _dbContext.ItemSets.Remove(itemSet);
            await _dbContext.SaveChangesAsync();
        }

        // TODO itemSetService: hand over, revoke, accept, decline
    }
}
