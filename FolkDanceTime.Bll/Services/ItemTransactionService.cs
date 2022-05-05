using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FolkDanceTime.Bll.Services
{
    public class ItemTransactionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemTransactionService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemTransactionDto>> GetItemTransactionsAsync()
        {
            // TODO create DetailedItemTransactionDto and use it here for admin view
            return await _dbContext.ItemTransactions
                .Include(t => t.Item)
                .ProjectTo<ItemTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemTransactionDto>> GetIncomingItemTransactionsAsync(string userId)
        {
            return await _dbContext.ItemTransactions
                .Include(t => t.Item)
                .Where(i => i.ReceiverUserId == userId && i.Status == Shared.Enums.Status.Pending)
                .ProjectTo<ItemTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemTransactionDto>> GetOutgoingItemTransactionsAsync(string userId)
        {
            return await _dbContext.ItemTransactions
                .Include(t => t.Item)
                .Where(i => i.SenderUserId == userId && i.Status == Shared.Enums.Status.Pending)
                .ProjectTo<ItemTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemTransactionDto> CreateTransactionAsync(int itemId, string senderUserId, string receiverUserId)
        {
            var isItemInTransaction = await _dbContext.ItemTransactions
                .AnyAsync(t => t.ItemId == itemId && t.Status == Shared.Enums.Status.Pending);

            if (isItemInTransaction)
            {
                throw new Exception();
            }

            if (senderUserId == receiverUserId)
            {
                throw new Exception();
            }

            var newTransaction = new ItemTransaction
            {
                ItemId = itemId,
                // maybe need to add Item here, or include it somehow for mapping to work
                Status = Shared.Enums.Status.Pending,
                SenderUserId = senderUserId,
                ReceiverUserId = receiverUserId,
                CreatedAt = DateTime.UtcNow,
            };

            _dbContext.ItemTransactions.Add(newTransaction);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ItemTransactionDto>(newTransaction);
        }

        public async Task<bool> RevokeTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemTransactions.FindAsync(id);

            if (transaction == null)
            {
                throw new Exception();
            }

            if (transaction.Status == Shared.Enums.Status.Pending && transaction.SenderUserId == userId)
            {
                transaction.Status = Shared.Enums.Status.Revoked;
                transaction.CompletedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeclineTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemTransactions.FindAsync(id);

            if (transaction == null)
            {
                throw new Exception();
            }

            if(transaction.Status == Shared.Enums.Status.Pending && transaction.ReceiverUserId == userId)
            {
                transaction.Status = Shared.Enums.Status.Declined;
                transaction.CompletedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AcceptTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemTransactions.Include(t => t.Item).FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                throw new Exception();
            }

            if (transaction.Status == Shared.Enums.Status.Pending && transaction.ReceiverUserId == userId)
            {
                transaction.Status = Shared.Enums.Status.Accepted;
                transaction.CompletedAt = DateTime.UtcNow;

                transaction.Item.OwnerUserId = userId;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
