using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;

namespace JsModels.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class JsModelMiddleware
    {
        public static JsModelMiddleware Instance = new JsModelMiddleware();

        private string _js;
        public string Path { get; private set; }
        public string VersionHash { get; private set; }

        public void Configure(JsModelsConfiguration configuration)
        {
            // save path
            Path = configuration.Path;

            // compute js
            var generator = new JsModelGenerator(configuration.Models);
            _js = generator.GenerateModels(configuration.Models);

            // minify
            _js = (new Minifier()).MinifyJavaScript(_js);

            // get version hash
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(_js);
            VersionHash = Convert.ToBase64String(SHA512.Create().ComputeHash(bytes));
        }

        public async Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.Response.ContentType = "application/javascript";
            await context.Response.WriteAsync(_js);
        }
    }
}
