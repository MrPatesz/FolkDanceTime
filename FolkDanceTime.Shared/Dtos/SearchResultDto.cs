namespace FolkDanceTime.Shared.Dtos
{
    public class SearchResultDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public UserDto OwnerUser { get; set; }
        public CategoryDto? Category { get; set; }
        public ItemSetDto? ItemSet { get; set; }
    }
}
