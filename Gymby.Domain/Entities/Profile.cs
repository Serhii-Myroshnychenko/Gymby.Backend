namespace Gymby.Domain.Entities;

public class Profile
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? Username { get; set; }
    public bool IsCoach { get; set; }
    public string? Description { get; set; }
    public string? PhotoAvatarPath { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TelegramUsername { get; set; }
}
