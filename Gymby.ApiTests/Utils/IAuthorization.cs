using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.ApiTests.Utils
{
    public interface IAuthorization
    {
        public Task<string> GetAccessTokenAsync(string username, string password);
    }
}
