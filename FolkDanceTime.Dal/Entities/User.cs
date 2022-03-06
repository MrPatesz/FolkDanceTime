namespace FolkDanceTime.Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }

        public virtual List<Item> Items { get; set; }
        public virtual List<ItemTransaction> ItemTransactions { get; set; }
        public virtual List<ItemSetTransaction> ItemSetTransactions { get; set; }
    }
}
