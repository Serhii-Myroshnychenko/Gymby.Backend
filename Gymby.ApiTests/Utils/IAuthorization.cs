namespace Gymby.ApiTests.Utils
{
    public interface IAuthorization
    {
        public Task<string> GetAccessTokenAsync(string username, string password);
    }
}
