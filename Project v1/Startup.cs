using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_v1.Startup))]
namespace Project_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
