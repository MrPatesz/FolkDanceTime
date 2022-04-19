namespace FolkDanceTime.Dal.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<PropertyValue> PropertyValues { get; set; }
    }
}
