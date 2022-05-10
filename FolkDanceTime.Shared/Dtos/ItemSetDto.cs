namespace FolkDanceTime.Shared.Dtos
{
    public class ItemSetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
