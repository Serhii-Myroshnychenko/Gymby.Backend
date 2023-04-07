using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class ProgramAccess
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProgramId { get; set; }
    public AccessType Type { get; set; }
    public bool IsFavorite { get; set; }
    public Program Program { get; set; } = null!;
}
