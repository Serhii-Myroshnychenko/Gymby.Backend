using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;

public class ProgramAccess
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public AccessType Type { get; set; }
    public bool IsFavorite { get; set; }
    public Program Program { get; set; } = null!;
}
