using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolkDanceTime.Dal.DbContext;
using FolkDanceTime.Dal.Entities;
using FolkDanceTime.Shared.Dtos;
using FolkDanceTime.Shared.Enums;
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
            // TODO use User's IncomingItemTransactions property (still have to filter for Pending)
            return await _dbContext.ItemTransactions
                .Include(t => t.Item)
                .Where(i => i.ReceiverUserId == userId && i.Status == Status.Pending)
                .ProjectTo<ItemTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ItemTransactionDto>> GetOutgoingItemTransactionsAsync(string userId)
        {
            // TODO use User's OutgoingItemTransactions property (still have to filter for Pending)
            return await _dbContext.ItemTransactions
                .Include(t => t.Item)
                .Where(i => i.SenderUserId == userId && i.Status == Status.Pending)
                .ProjectTo<ItemTransactionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ItemTransactionDto> CreateTransactionAsync(int itemId, string senderUserId, string receiverUserId)
        {
            var isItemInTransaction = await _dbContext.ItemTransactions
                .AnyAsync(t => t.ItemId == itemId && t.Status == Status.Pending);

            if (isItemInTransaction)
            {
                throw new Exception();
            }

            if (senderUserId == receiverUserId)
            {
                throw new Exception();
            }

            var item = _dbContext.Items.Find(itemId);

            var newTransaction = new ItemTransaction
            {
                ItemId = itemId,
                Item = item,
                Status = Status.Pending,
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

        public async Task<bool> DeclineTransactionAsync(int id, string userId)
        {
            var transaction = await _dbContext.ItemTransactions.FindAsync(id);

            if (transaction == null)
            {
                throw new Exception();
            }

            if(transaction.Status == Status.Pending && transaction.ReceiverUserId == userId)
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
