using FolkDanceTime.Shared.Enums;

namespace FolkDanceTime.Shared.Dtos
{
    public class ItemSetTransactionDto
    {
        public int Id { get; set; }
        public string ItemSetName { get; set; }
        public List<ItemDto> Items { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public UserDto ReceiverUser { get; set; }
        public UserDto SenderUser { get; set; }
    }
}
