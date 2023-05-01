using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MangaHub.Startup))]
namespace MangaHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
