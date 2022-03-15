using Microsoft.AspNetCore.Identity;

namespace FolkDanceTime.Dal.Entities
{
    public class User : IdentityUser
    {
        public virtual List<Item> Items { get; set; }
        public virtual List<ItemTransaction> IncomingItemTransactions { get; set; }
        public virtual List<ItemTransaction> OutgoingItemTransactions { get; set; }
        public virtual List<ItemSetTransaction> IncomingItemSetTransactions { get; set; }
        public virtual List<ItemSetTransaction> OutgoingItemSetTransactions { get; set; }
    }
}
