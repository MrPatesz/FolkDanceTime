namespace FolkDanceTime.Dal.Entities
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ItemId { get; set; }
        public int PropertyId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Property Property { get; set; }
    }
}
