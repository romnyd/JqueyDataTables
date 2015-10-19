using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JqueyDataTables.Startup))]
namespace JqueyDataTables
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
