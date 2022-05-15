using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using FolkDanceTime.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class ItemSetTransactionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemSetTransactionService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemSetTransactionDto>> GetItemSetTransactionsAsync()
        {
            // TODO DetailedItemSetTransacitonDto and ItemSetTransactionDto
            return await _dbContext.ItemSetTransactions
                .IgnoreQueryFilters()
                .ProjectTo<ItemSetTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemSetTransactionDto>> GetIncomingItemSetTransactionsAsync(string userId)
        {
            // TODO use User's IncomingItemSetTransactions property (still have to filter for Pending)
            return await _dbContext.ItemSetTransactions
                .Where(i => i.ReceiverUserId == userId && i.Status == Status.Pending)
                .ProjectTo<ItemSetTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemSetTransactionDto>> GetOutgoingItemSetTransactionsAsync(string userId)
        {
            // TODO use User's OutgoingItemTransactions property (still have to filter for Pending)
            return await _dbContext.ItemSetTransactions
                .Where(i => i.SenderUserId == userId && i.Status == Status.Pending)
                .ProjectTo<ItemSetTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemSetTransactionDto> CreateItemSetTransactionAsync(int itemSetId, string senderUserId, string receiverUserId)
        {
            if (senderUserId == receiverUserId)
            {
                throw new Exception();
            }

            var isItemSetInTransaction = await _dbContext.ItemSetTransactions
                .AnyAsync(t => t.ItemSetId == itemSetId && t.Status == Status.Pending);

            if (isItemSetInTransaction)
            {
                throw new Exception();
            }

            var itemSet = await _dbContext.ItemSets
                .Include(set => set.Items)
                .SingleAsync(set => set.Id == itemSetId);

            var newItemSetTransaction = new ItemSetTransaction
            {
                ItemSetId = itemSetId,
                ItemSet = itemSet,
                Status = Status.Pending,
                SenderUserId = senderUserId,
                ReceiverUserId = receiverUserId,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.ItemSetTransactions.AddAsync(newItemSetTransaction);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemSetTransactionDto>(newItemSetTransaction);
        }

        public async Task<bool> RevokeItemSetTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemSetTransactions.SingleAsync(t => t.Id == id);

            if (transaction.Status == Status.Pending && transaction.SenderUserId == userId)
            {
                transaction.Status = Status.Revoked;
                transaction.CompletedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeclineItemSetTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemSetTransactions.SingleAsync(t => t.Id == id);

            if (transaction.Status == Status.Pending && transaction.ReceiverUserId == userId)
            {
                transaction.Status = Status.Declined;
                transaction.CompletedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AcceptItemSetTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemSetTransactions
                .Include(t => t.ItemSet)
                .ThenInclude(t => t.Items)
                .SingleAsync(t => t.Id == id);

            if (transaction.Status != Status.Pending || transaction.ReceiverUserId != userId) return false;

            var itemTransactions = new List<ItemTransaction>();

            transaction.Status = Status.Accepted;
            transaction.CompletedAt = DateTime.UtcNow;
            transaction.ItemSet.OwnerUserId = userId;
            transaction.ItemSet.Items.ForEach(i => {
                i.OwnerUserId = userId;

                itemTransactions.Add(new ItemTransaction
                {
                    ItemId = i.Id,
                    Item = i,
                    Status = Status.Accepted,
                    SenderUserId = transaction.SenderUserId,
                    ReceiverUserId = transaction.ReceiverUserId,
                    CreatedAt = transaction.CreatedAt,
                    CompletedAt = DateTime.UtcNow,
                });
            });

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
