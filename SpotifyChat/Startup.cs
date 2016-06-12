using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpotifyChat.Startup))]
namespace SpotifyChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
