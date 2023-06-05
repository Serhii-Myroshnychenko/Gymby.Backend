using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.ProgramDays
{
    public class ProgramDaysCommandTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public ProgramDaysCommandTestFixture()
        {
            Context = ProgramDaysContextFactory.Create();
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
            ProgramDaysContextFactory.Destroy(Context);
        }
    }
}
