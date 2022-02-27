namespace FolkDanceTime.Bll.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Item> Items { get; set; }
        public virtual List<Property> Properties { get; set; }
        public virtual List<PropertyToCategory> CategoryToProperties { get; set; }
    }
}
