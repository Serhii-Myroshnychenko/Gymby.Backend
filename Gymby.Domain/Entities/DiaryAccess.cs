using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class DiaryAccess
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DiaryId { get; set; }
    public AccessType Type { get; set; }
}
