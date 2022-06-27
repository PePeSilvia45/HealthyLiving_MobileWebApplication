using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthyLiving.Startup))]
namespace HealthyLiving
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
