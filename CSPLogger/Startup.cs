using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSPLogger.Startup))]
namespace CSPLogger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
