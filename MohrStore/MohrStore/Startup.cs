using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MohrStore.Startup))]
namespace MohrStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
