[![Build Status](https://dev.azure.com/ucdavis/JsModels.Net/_apis/build/status/ucdavis.JsModels.Net?branchName=master)](https://dev.azure.com/ucdavis/JsModels.Net/_build/latest?definitionId=21&branchName=master)

# JsModels.Net
Javascript model building for .NET apps

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapJsModels(new JsModelsConfiguration()
            {
                Types = typeof(MyModelsAssembly).Assembly.ExportedTypes.ToArray()
            });
        }
    }
