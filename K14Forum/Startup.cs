using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(K14Forum.Startup))]
namespace K14Forum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
