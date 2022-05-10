namespace FolkDanceTime.Dal.Entities
{
    public class ItemSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerUserId { get; set; }

        public virtual User OwnerUser { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<ItemSetTransaction> ItemSetTransactions { get; set; }
    }
}
