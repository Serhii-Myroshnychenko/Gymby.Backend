using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.Exercise
{
    public class ProgramExerciseCommandTestFixture
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public ProgramExerciseCommandTestFixture()
        {
            Context = ProgramExerciseContextFactory.Create();
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
            ProgramExerciseContextFactory.Destroy(Context);
        }
    }
}
