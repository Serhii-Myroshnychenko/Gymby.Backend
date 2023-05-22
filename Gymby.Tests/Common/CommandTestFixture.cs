using AutoMapper;
using Azure.Storage.Blobs;
using Gymby.Application.Common.Mappings;
using Gymby.UnitTests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
