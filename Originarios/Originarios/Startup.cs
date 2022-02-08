using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Originarios.Startup))]
namespace Originarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}