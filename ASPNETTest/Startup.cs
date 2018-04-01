using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPNETTest.Startup))]
namespace ASPNETTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
