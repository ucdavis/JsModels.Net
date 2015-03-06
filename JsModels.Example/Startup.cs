using System.Linq;
using Microsoft.Owin;
using ModelPrototype.Owin;
using Owin;
using GiftModels;

[assembly: OwinStartupAttribute(typeof(JsModels.Example.Startup))]
namespace JsModels.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapJsModels(new JsModelsConfiguration()
            {
                Types = typeof(GiftDetails).Assembly.ExportedTypes.ToArray()
            });
        }
    }
}
