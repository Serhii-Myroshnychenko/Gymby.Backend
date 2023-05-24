namespace Gymby.UnitTests
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public TestCommandBase()
        {
            Context = ProfileContextFactory.Create();
            Context = PhotoContextFactory.Create();
        } 

        public void Dispose()
        {
            ProfileContextFactory.Destroy(Context);
            PhotoContextFactory.Destroy(Context);
        }
    }
}
