﻿
namespace Gymby.Application.CommandModels;

public class ProgramDayCM
{
    public string Name { get; set; } = null!;
    public List<ExerciseCM>? Exercises { get; set; }
}
