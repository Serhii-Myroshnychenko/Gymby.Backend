using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class Program
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string Description { get;set; } = null!;
    public Level Level { get; set; }
    public ProgramType Type { get; set; }
}
