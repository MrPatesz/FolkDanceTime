namespace FolkDanceTime.Dal.Entities
{
    public class Item : ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureFilename { get; set; }
        public string OwnerUserId { get; set; }
        public int? CategoryId { get; set; }
        public int? ItemSetId { get; set; }

        public virtual User OwnerUser { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ItemSet? ItemSet { get; set; }
        public virtual List<ItemTransaction> ItemTransactions { get; set; }
        public virtual List<PropertyValue> PropertyValues { get; set; }
    }
}
