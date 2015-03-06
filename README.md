[![Build status](https://ci.appveyor.com/api/projects/status/1a6n977ipcyt1sl0?svg=true)](https://ci.appveyor.com/project/UCNETAdmin/jsmodels-net)

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
