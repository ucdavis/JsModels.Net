using System.Text;
using System.Web;
using JsModels.Owin;

namespace JsModels.Html
{
    public static class JsModelsScripts
    {
        private const string TagFormat = "<script src=\"{0}?v={1}\"></script>";

        /// <summary>
        /// Renders script tags for the following paths.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML string containing the script tag or tags for the bundle.
        /// </returns>
        public static IHtmlString Render()
        {
            var middleware = JsModelMiddleware.Instance;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(TagFormat, middleware.Path, middleware.VersionHash);

            return new HtmlString(stringBuilder.ToString());
        }
    }
}
