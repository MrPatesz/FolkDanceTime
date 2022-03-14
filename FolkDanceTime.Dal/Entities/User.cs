using Microsoft.AspNetCore.Identity;

namespace FolkDanceTime.Dal.Entities
{
    public class User : IdentityUser
    {
        public virtual List<Item> Items { get; set; }
        public virtual List<ItemTransaction> ItemTransactions { get; set; }
        public virtual List<ItemSetTransaction> ItemSetTransactions { get; set; }
    }
}
