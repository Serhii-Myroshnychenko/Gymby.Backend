using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Exercises.Commands.CreateDiaryExercise;

public class CreateDiaryExerciseCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string DiaryId { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public string? ProgramDayId { get; set;}
    public DateTime Date { get; set; }
    public string Name { get; set; } = null!;
}
