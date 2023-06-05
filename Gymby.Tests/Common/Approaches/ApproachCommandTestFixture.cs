using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.Approaches
{
    public class ApproachCommandTestFixture
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public ApproachCommandTestFixture()
        {
            Context = ApproachesContextFactory.Create();
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
            ApproachesContextFactory.Destroy(Context);
        }
    }
}
