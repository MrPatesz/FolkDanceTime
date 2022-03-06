namespace FolkDanceTime.Dal.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<PropertyValue> PropertyValues { get; set; }
        public virtual List<PropertyToCategory> PropertyToCategories { get; set; }
    }
}
