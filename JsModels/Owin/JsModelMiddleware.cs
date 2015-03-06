using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin;

namespace ModelPrototype.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class JsModelMiddleware
    {
        private readonly string _js;

        public JsModelMiddleware(JsModelsConfiguration configuration)
        {
            var generator = new JsModelGenerator(configuration.Types);
            _js = generator.GenerateModels(configuration.Types);

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
