namespace Gymby.Domain.Entities
{
    public class Diary
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public ICollection<DiaryAccess> DiaryAccesses { get; set; } = null!;
        public ICollection<DiaryDay> DiaryDays { get; set; } = null!;
    }
}
