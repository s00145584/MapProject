using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MapProject.Startup))]
namespace MapProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
