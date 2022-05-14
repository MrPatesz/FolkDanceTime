namespace FolkDanceTime.Shared.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PropertyDto> Properties { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
