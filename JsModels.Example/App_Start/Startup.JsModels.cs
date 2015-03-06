using JsModels.Example.Models;
using JsModels.Owin;
using Owin;

namespace JsModels.Example
{
    public partial class Startup
    {
        public void ConfigureJsModels(IAppBuilder app)
        {
            app.MapJsModels(new JsModelsConfiguration()
            {
                Types = new[] { typeof(SampleModel), typeof(SampleChildModel) }
            });
        }
    }
}
