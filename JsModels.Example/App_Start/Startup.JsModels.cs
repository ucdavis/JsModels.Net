using JsModels.Example.Models;
using JsModels.Owin;
using Owin;

namespace JsModels.Example
{
    public partial class Startup
    {
        public void ConfigureJsModels(IAppBuilder app)
        {
            var config = new JsModelsConfiguration();
            config.Models.AddRange(new[] {typeof (SampleModel), typeof (SampleChildModel)});

            app.MapJsModels(config);
        }
    }
}
