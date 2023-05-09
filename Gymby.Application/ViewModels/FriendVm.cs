using Gymby.Domain.Enums;

namespace Gymby.Application.ViewModels;

public class FriendVm
{
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
    public Status Status { get; set; }
    public ProfileVm Profile { get; set; } = null!;
}
