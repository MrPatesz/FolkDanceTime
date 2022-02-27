namespace FolkDanceTime.Bll.Entities
{
    public class ItemTransaction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ReceiverUserId { get; set; }

        public virtual Item Item { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}
