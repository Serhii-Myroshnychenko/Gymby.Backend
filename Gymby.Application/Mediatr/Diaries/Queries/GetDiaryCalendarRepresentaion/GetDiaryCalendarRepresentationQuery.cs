using Gymby.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
namespace Gymby.Application.Mediatr.Diaries.Queries.GetDiaryCalendarRepresentaion;

public class GetDiaryCalendarRepresentationQuery 
    : IRequest<List<DiaryCalendarRepresentationVm>>
{
    public string UserId { get; set; } = null!;
    public string? DiaryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
