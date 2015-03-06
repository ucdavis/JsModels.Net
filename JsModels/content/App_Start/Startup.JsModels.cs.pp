using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsModels.Owin;
using Owin;

namespace $rootnamespace$
{
    public partial class Startup
    {
        public void ConfigureJsModels(IAppBuilder app)
        {
            app.MapJsModels(new JsModelsConfiguration()
            {
                Types = new[] { typeof(SampleModel), typeof(SampleModel2), typeof(etc) }
            });
        }
    }
}
