using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieDirectory.Startup))]
namespace MovieDirectory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
