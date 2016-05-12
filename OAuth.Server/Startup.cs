using Microsoft.Owin;
using OAuth.Server;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}