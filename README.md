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
