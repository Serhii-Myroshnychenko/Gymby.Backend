using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.UnitTests.Services;

namespace Gymby.UnitTests.Common
{
    public class PhotoCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public PhotoCommandTestFixture()
        {
            Context = PhotoContextFactory.Create();
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
            PhotoContextFactory.Destroy(Context);
        }
    }
}
