using Gymby.Application.Interfaces;
using Gymby.Application.Utils;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

public class CreateProfileHandler 
    : IRequestHandler<CreateProfileCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProfileHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(CreateProfileCommand createProfile,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == createProfile.UserId, cancellationToken);

        if(user != null)
        {
            return Unit.Value;
        }

        var username = UsernameHandler.GenerateUsername();
        var existingUsernames = new HashSet<string>(_dbContext.Profiles.Select(p => p.Username)!);

        while (existingUsernames.Contains(username))
        {
            username = UsernameHandler.GenerateUsername();
        }

        var profile = new Profile()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = createProfile.UserId,
            Email = createProfile.Email,
            FirstName = "FirstName",
            LastName = "LastName",
            Username = username,
            InstagramUrl = string.Empty,
            TelegramUsername = string.Empty,
            Description = string.Empty,
            IsCoach = false
        };

        await _dbContext.Profiles.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
