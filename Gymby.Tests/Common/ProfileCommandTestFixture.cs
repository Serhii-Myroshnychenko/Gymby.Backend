using AutoMapper;
using Gymby.Application.Common.Mappings;
namespace Gymby.UnitTests.Common
{
    public  class ProfileCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

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
        }

        public void Dispose()
        {
            ProfileContextFactory.Destroy(Context);
        }
    }

    //[CollectionDefinition("CommandCollection")]
    //public class CommandCollection : ICollectionFixture<CommandTestFixture> { }
}
