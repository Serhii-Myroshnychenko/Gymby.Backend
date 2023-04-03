using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;

public class Friend
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public Status Status { get; set; }
}
