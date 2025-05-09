using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NUglify;

namespace JsModels.Middleware
{
    public class JsModelMiddleware
    {
        private readonly RequestDelegate _next;

        private string _js;
        private byte[] _jsCompressed;

        public string Path { get; private set; }
        public string VersionHash { get; private set; }

        public JsModelMiddleware(RequestDelegate next, JsModelsConfiguration configuration)
        {
            _next = next;

            // save path
            Path = configuration.Path;

            // compute js
            var generator = new JsModelGenerator(configuration.Models);
            _js = generator.GenerateModels(configuration.Models);

            // minify
            var jsResult = Uglify.Js(_js);
            if (jsResult.HasErrors)
            {
                throw new Exception("Error minifying JavaScript: " + string.Join(", ", jsResult.Errors));
            }
            _js = jsResult.Code;

            // get version hash
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(_js);
            VersionHash = Convert.ToBase64String(SHA512.Create().ComputeHash(bytes));

            // compress and read out to byte array
            using (var ms = new MemoryStream())
            {
                using (var stream = new GZipStream(ms, CompressionLevel.Optimal, false))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                _jsCompressed = ms.ToArray();
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Request.Path.Equals(Path, StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            context.Response.ContentType = "application/javascript";
            context.Response.Headers["Cache-Control"] = "max-age=2592000";

            var acceptEncoding = context.Request.Headers["Accept-Encoding"].ToString();
            bool supportGZip = acceptEncoding?.Split(',').Any(v => v.Trim() == "gzip") == true;

            if (supportGZip)
            {
                context.Response.Headers["Content-Encoding"] = "gzip";
                await context.Response.Body.WriteAsync(_jsCompressed, 0, _jsCompressed.Length);
            }
            else
            {
                await context.Response.WriteAsync(_js);
            }
        }
    }
}
