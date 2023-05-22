using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public TestCommandBase()
        {
            Context = ProfileContextFactory.Create();
        } 

        public void Dispose()
        {
            ProfileContextFactory.Destroy(Context);
        }
    }
}
