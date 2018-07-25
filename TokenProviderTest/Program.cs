
/* This program is for test purpose.
 * Testing AuthTokenService library */

using AuthTokenService;
using System.Threading;

namespace TokenProviderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenProvider = TokenProvider.Instance;

            tokenProvider.SignInAsync("sev", "password").RunSynchronously();

            while(true)
            {
                Thread.Sleep(50);
            }
        }
    }
}
