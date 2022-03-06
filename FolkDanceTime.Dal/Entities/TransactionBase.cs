namespace FolkDanceTime.Dal.Entities
{
    public class TransactionBase
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int ReceiverUserId { get; set; }
        public int SenderUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public virtual User ReceiverUser { get; set; }
        public virtual User SenderUser { get; set; }
    }
}
