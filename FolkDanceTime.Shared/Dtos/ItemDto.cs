namespace FolkDanceTime.Shared.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureFilename { get; set; }

        public List<PropertyValueDto> Properties { get; set; }
    }
}
