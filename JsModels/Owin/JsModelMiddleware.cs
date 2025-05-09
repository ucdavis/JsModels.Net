using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace JsModels.Owin
{
    public class JsModelMiddleware
    {
        public static JsModelMiddleware Instance = new JsModelMiddleware();

        private string _js;
        private byte[] _jsCompressed;

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

        public async Task Invoke(IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.Response.ContentType = "application/javascript";
            context.Response.Headers.Add("Cache-Control", new[] { "max-age=2592000" });

            var acceptEncoding = context.Request.Headers.GetValues("Accept-Encoding");
            bool supportGZip = acceptEncoding != null && acceptEncoding.Any(v => v.Split(',').Contains("gzip"));
            if (!supportGZip)
            {
                await context.Response.WriteAsync(_js);
                return;
            }

            await context.Response.WriteAsync(_jsCompressed);
            context.Response.Headers.Add("Content-encoding", new []{"gzip"});
        }
    }
}
