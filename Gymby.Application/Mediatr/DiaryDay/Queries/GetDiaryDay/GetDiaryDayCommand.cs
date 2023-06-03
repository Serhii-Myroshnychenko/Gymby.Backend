using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.DiaryDay.Queries.GetDiaryDay;

public class GetDiaryDayCommand : IRequest<DiaryDayVm>
{
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? DiaryId { get; set; }
}
