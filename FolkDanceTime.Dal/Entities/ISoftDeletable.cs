namespace FolkDanceTime.Dal.Entities
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
