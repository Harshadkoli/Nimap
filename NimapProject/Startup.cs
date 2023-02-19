using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NimapProject.Startup))]
namespace NimapProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
