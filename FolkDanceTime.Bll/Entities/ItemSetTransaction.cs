namespace FolkDanceTime.Bll.Entities
{
    public class ItemSetTransaction
    {
        public int Id { get; set; }
        public int ItemSetId { get; set; }
        public int ReceiverUserId { get; set; }

        public virtual ItemSet ItemSet { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}
