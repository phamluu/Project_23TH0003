using Microsoft.AspNetCore.Mvc.Rendering;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string? action = null)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"]?.ToString();
            var routeController = routeData.Values["controller"]?.ToString();

            bool isControllerMatch = string.Equals(controller, routeController, StringComparison.OrdinalIgnoreCase);
            bool isActionMatch = string.IsNullOrEmpty(action) || string.Equals(action, routeAction, StringComparison.OrdinalIgnoreCase);

            return (isControllerMatch && isActionMatch) ? "active" : "";

        }

        public static string IsMenuGroupActive(this IHtmlHelper htmlHelper, params string[] controllers)
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            var routeController = routeData.Values["controller"]?.ToString();

            return controllers.Any(c => string.Equals(c, routeController, StringComparison.OrdinalIgnoreCase)) ? "show" : "";
        }

    }
}
