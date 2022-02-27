namespace FolkDanceTime.Bll.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? ItemSetId { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ItemSet ItemSet { get; set; }
        public virtual ItemTransaction ItemTransaction { get; set; }
        public virtual List<PropertyValue> PropertyValues { get; set; }
    }
}
