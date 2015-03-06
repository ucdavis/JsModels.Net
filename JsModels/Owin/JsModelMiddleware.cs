using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;

namespace JsModels.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class JsModelMiddleware
    {
        private readonly string _js;

        public JsModelMiddleware(JsModelsConfiguration configuration)
        {
            var generator = new JsModelGenerator(configuration.Models);
            _js = generator.GenerateModels(configuration.Models);

            // minify
            _js = (new Minifier()).MinifyJavaScript(_js);
        }

        public async Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //return base.ProcessRequest(context);

            context.Response.ContentType = "application/javascript";
            await context.Response.WriteAsync(_js);
        }
    }
}
