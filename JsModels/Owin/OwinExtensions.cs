using ModelPrototype.Owin;
using System.Diagnostics.CodeAnalysis;


// ReSharper disable once CheckNamespace
namespace Owin
{
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin", Justification = "The owin namespace is for consistentcy.")]
    public static class OwinExtensions
    {
        /// <summary>
        /// Maps JsModelsto the app builder pipeline at "/jsmodels".
        /// </summary>
        /// <param name="appBuilder">The app builder</param>
        /// <param name="configuration"></param>
        public static IAppBuilder MapJsModels(this IAppBuilder appBuilder, JsModelsConfiguration configuration)
        {
            return appBuilder.MapJsBuilder("/jsmodels", configuration);
        }

        public static IAppBuilder MapJsBuilder(this IAppBuilder appBuilder, string path,
            JsModelsConfiguration configuration)
        {
            return appBuilder.Map(path, subBuilder => Configuration(subBuilder, configuration));
        }

        private static void Configuration(IAppBuilder appBuilder, JsModelsConfiguration configuration)
        {
            var middleware = new JsModelMiddleware(configuration);
            appBuilder.Run(middleware.Invoke);
        }
    }
}
