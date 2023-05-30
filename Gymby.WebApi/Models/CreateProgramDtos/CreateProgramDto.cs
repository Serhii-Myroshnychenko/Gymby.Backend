using Gymby.Domain.Enums;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Level Level { get; set; }
    public ProgramType Type { get; set; }
    public List<CreateProgramProgramDayDto>? ProgramDays { get; set; }
}
