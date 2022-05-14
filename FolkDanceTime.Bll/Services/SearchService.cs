using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Shared.Dtos;
using FolkDanceTime.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SearchService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SearchResultDto>> SearchItemsAsync(string term, SearchBy searchBy)
        {
            return searchBy switch
            {
                SearchBy.All => await SearchItemsByAllAsync(term),
                SearchBy.Item => await SearchItemsByNameAsync(term),
                SearchBy.Category => await SearchItemsByCategoryAsync(term),
                SearchBy.User => await SearchItemsByUserAsync(term),
                _ => await SearchItemsByAllAsync(""),
            };
        }

        private async Task<List<SearchResultDto>> SearchItemsByAllAsync(string term)
        {
            if (term == null)
            {
                term = "";
            }
            return await _dbContext.Items
                .Where(item => item.Name.ToLower().Contains(term.ToLower()) ||
                               item.Category.Name.ToLower().Contains(term.ToLower()) ||
                               item.OwnerUser.UserName.ToLower().Contains(term.ToLower()))
                .ProjectTo<SearchResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        private async Task<List<SearchResultDto>> SearchItemsByNameAsync(string term)
        {
            if (term == null)
            {
                term = "";
            }
            return await _dbContext.Items
                .Where(item => item.Name.ToLower().Contains(term.ToLower()))
                .ProjectTo<SearchResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        private async Task<List<SearchResultDto>> SearchItemsByCategoryAsync(string term)
        {
            if (term == null)
            {
                term = "";
            }
            return await _dbContext.Items
                .Where(item => item.Category.Name.ToLower().Contains(term.ToLower()))
                .ProjectTo<SearchResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        private async Task<List<SearchResultDto>> SearchItemsByUserAsync(string term)
        {
            if (term == null)
            {
                term = "";
            }
            return await _dbContext.Items
                .Where(item => item.OwnerUser.UserName.ToLower().Contains(term.ToLower()))
                .ProjectTo<SearchResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
