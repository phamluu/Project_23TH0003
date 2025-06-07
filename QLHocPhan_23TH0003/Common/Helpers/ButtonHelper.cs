using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ModalButton(this IHtmlHelper htmlHelper, string text, string btnClass, string extraClass, string modalId)
        {
            var button = new TagBuilder("button");
            button.Attributes["type"] = "button";
            button.AddCssClass($"btn {btnClass} {extraClass}");
            button.Attributes["data-bs-toggle"] = "modal";
            button.Attributes["data-bs-target"] = $"#{modalId}";
            button.InnerHtml.Append(text);
            return button;
        }
        public static IHtmlContent DeleteButton(this IHtmlHelper htmlHelper, object id, string url, string btnClass = "btn-danger btn-sm")
        {
            var button = new TagBuilder("button");
            button.Attributes["type"] = "button";
            button.AddCssClass($"btn {btnClass} btn-delete");
            button.Attributes["data-id"] = id?.ToString();
            button.Attributes["data-url"] = url;
            button.InnerHtml.Append("Xóa");
            return button;
        }
        public static IHtmlContent EditButton(this IHtmlHelper htmlHelper, string controller, object id, string action = "Edit", string btnClass = "btn-edit", string text = "Cập nhật")
        {
            // ✅ Lấy IUrlHelper thông qua DI để tương thích với endpoint routing
            var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(htmlHelper.ViewContext);

            var href = urlHelper.Action(action, controller, new { id });

            var anchor = new TagBuilder("a");
            anchor.Attributes["href"] = href;
            anchor.AddCssClass($"btn {btnClass}");
            anchor.InnerHtml.Append(text);

            return anchor;
        }

    }
}
