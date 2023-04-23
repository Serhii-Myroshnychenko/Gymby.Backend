using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class DiaryAccess
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string DiaryId { get; set; } = null!;
    public AccessType Type { get; set; }
    public Diary Diary { get; set; } = null!;
}
