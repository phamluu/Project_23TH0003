using Microsoft.AspNetCore.Mvc.Rendering;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string action, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"]?.ToString();
            var routeController = routeData.Values["controller"]?.ToString();

            return (string.Equals(action, routeAction, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(controller, routeController, StringComparison.OrdinalIgnoreCase))
                    ? "active" : "";
        }
    }
}
