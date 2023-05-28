using AutoMapper;
using Gymby.Application.Common.Mappings;
namespace Gymby.UnitTests.Common.Friends
{
    public class FriendCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public FriendCommandTestFixture()
        {
            Context = FriendContextFactory.Create();
            FileService = new FileService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            FriendContextFactory.Destroy(Context);
        }
    }
}
