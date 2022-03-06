namespace FolkDanceTime.Dal.Entities
{
    public class PropertyToCategory
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }

        public virtual Property Property { get; set; }
        public virtual Category Category { get; set; }
    }
}
