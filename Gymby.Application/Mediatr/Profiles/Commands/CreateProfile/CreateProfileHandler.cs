using Gymby.Application.Interfaces;
using Gymby.Application.Utils;
using Gymby.Domain.Entities;
using MediatR;

namespace Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

public class CreateProfileHandler 
    : IRequestHandler<CreateProfileCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProfileHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<string> Handle(CreateProfileCommand createProfile, 
        CancellationToken cancellationToken)
    {
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
            Username = username,
            IsCoach = false
            
        };

        await _dbContext.Profiles.AddAsync(profile, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return profile.Id;
    }
}
