using AutoMapper;
using Gymby.Application.Common.Mappings;

namespace Gymby.UnitTests.Common.ExercisePrototype
{
    public class ExercisePrototypeQueryTestFixture
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;

        public ExercisePrototypeQueryTestFixture()
        {
            Context = ExercisePrototypeContextFactory.Create();
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
            ExercisePrototypeContextFactory.Destroy(Context);
        }
    }
}
