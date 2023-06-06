using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;
using Microsoft.Extensions.DependencyInjection;

namespace Gymby.UnitTests.Common.Diaries
{
    public class DiaryCommandTestSixture
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;
        public IFileService FileService;
        public IMediator Mediator;

        public DiaryCommandTestSixture()
        {
            Context = DiaryCommandContextFactory.Create();
            FileService = new FileService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IApplicationDbContext>(provider => Context)
                .AddTransient<IMapper>(provider => Mapper)
                 .AddTransient<IMediator, Mediator>()
                 .AddTransient<ServiceFactory>(sp => sp.GetService)
                 .AddTransient<IRequestHandler<ImportProgramDayCommand, Unit>, ImportProgramDayHandler>() 
                 .BuildServiceProvider();

            Mediator = serviceProvider.GetService<IMediator>();
        }

        public void Dispose()
        {
            DiaryCommandContextFactory.Destroy(Context);
        }
    }
}
