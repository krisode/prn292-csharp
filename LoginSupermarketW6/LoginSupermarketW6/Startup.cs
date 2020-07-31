using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginSupermarketW6.Startup))]
namespace LoginSupermarketW6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
