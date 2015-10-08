using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuerraDeTweets.Startup))]
namespace GuerraDeTweets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
