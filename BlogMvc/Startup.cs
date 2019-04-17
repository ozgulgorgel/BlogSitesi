using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogMvc.Startup))]
namespace BlogMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
