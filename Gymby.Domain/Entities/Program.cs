﻿using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class Program
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string Description { get;set; } = null!;
    public Level Level { get; set; }
    public ProgramType Type { get; set; }
    public ICollection<ProgramAccess> ProgramAccesses { get; set; } = null!;
    public ICollection<ProgramDay> ProgramDays { get; set; } = null!;
}
