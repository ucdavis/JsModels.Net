
using JsModels.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace JsModels.Html
{
    public class JsModelsScriptsViewComponent : ViewComponent
    {
        private readonly JsModelMiddleware _middleware;

        public JsModelsScriptsViewComponent(JsModelMiddleware middleware)
        {
            _middleware = middleware;
        }

        public IViewComponentResult Invoke()
        {
            var tag = $"<script src=\"{_middleware.Path}?v={_middleware.VersionHash}\"></script>";
            return new HtmlContentViewComponentResult(new Microsoft.AspNetCore.Html.HtmlString(tag));
        }
    }
}
