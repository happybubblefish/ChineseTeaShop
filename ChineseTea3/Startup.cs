using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChineseTea3.Startup))]
namespace ChineseTea3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
