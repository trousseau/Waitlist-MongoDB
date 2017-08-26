using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WaitlListMongoDB.Startup))]
namespace WaitlListMongoDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
