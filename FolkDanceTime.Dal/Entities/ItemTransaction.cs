namespace FolkDanceTime.Dal.Entities
{
    public class ItemTransaction : TransactionBase
    {
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
