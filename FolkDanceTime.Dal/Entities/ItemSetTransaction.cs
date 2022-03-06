namespace FolkDanceTime.Dal.Entities
{
    public class ItemSetTransaction : TransactionBase
    {
        public int ItemSetId { get; set; }

        public virtual ItemSet ItemSet { get; set; }
    }
}
