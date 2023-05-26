using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.Measurements
{
    public class MeasurementCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public MeasurementCommandTestFixture()
        {
            Context = MeasurementContextFactory.Create();
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
            MeasurementContextFactory.Destroy(Context);
        }
    }
}
