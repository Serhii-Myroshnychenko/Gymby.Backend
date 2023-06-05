using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.Programs
{
    public class ProgramCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public ProgramCommandTestFixture()
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
}
