using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;

namespace Gymby.UnitTests.Common.Profiles
{
    public class ProfileCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;
        public IMediator Mediator;

        public ProfileCommandTestFixture()
        {
            Context = ProfileContextFactory.Create();

            FileService = new FileService();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();

            var merdiator = new Mock<IMediator>();

            var friendsList = new List<ProfileVm>();

            merdiator
                .Setup(m => m.Send(It.IsAny<GetMyFriendsListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(friendsList);
            Mediator = merdiator.Object;
        }

        public void Dispose()
        {
            ProfileContextFactory.Destroy(Context);
        }
    }
}
