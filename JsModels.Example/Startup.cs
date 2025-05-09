
[assembly: OwinStartupAttribute(typeof(JsModels.Example.Startup))]
namespace JsModels.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureJsModels(app);
        }
    }
}
