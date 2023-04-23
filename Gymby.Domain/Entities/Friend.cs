using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;

public class Friend
{
    public string Id { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
    public Status Status { get; set; }
}
