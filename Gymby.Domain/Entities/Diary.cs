namespace Gymby.Domain.Entities
{
    public class Diary
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public virtual ICollection<DiaryAccess> DiaryAccesses { get; set; } = null!;
        public virtual ICollection<DiaryDay> DiaryDays { get; set; } = null!;
    }
}
