using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.UnitTests.Services;

namespace Gymby.UnitTests.Common
{
    public  class CommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public CommandTestFixture()
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

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandTestFixture> { }
}
