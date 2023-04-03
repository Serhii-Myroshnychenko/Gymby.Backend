namespace Gymby.Domain.Entities;
public class ProgramDay
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public string Name { get; set; } = null!;
}
