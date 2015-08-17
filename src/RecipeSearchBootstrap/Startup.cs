using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeSearchBootstrap.Startup))]
namespace RecipeSearchBootstrap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
