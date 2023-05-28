using AutoMapper;

namespace Gymby.UnitTests
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        protected readonly IMapper Mapper;
        protected readonly IFileService FileService;

        public TestCommandBase(IMapper mapper, IFileService fileService)
        {
            Context = ProfileContextFactory.Create();
            Context = PhotoContextFactory.Create();
            Context = MeasurementContextFactory.Create();
            Mapper = mapper;
            FileService = fileService;
        } 

        public void Dispose()
        {
            ProfileContextFactory.Destroy(Context);
            PhotoContextFactory.Destroy(Context);
            MeasurementContextFactory.Destroy(Context);
        }
    }
}
