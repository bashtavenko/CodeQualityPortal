using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace CodeQualityPortal.Common
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MenuItem(this HtmlHelper helper, string text, string action, string controller)
        {
            var tag = new StringBuilder("<li");
            if (RouteMatch(helper, action, controller))
            {
                tag.Append(" class='active'");
            }
            tag.Append("/>");
            tag.Append(helper.ActionLink(text, action, controller));
            tag.Append("</li>");
            return new MvcHtmlString(tag.ToString());
        }
        
        public static bool RouteMatch(this HtmlHelper helper, string action, string controller)
        {
            return RouteMatch(helper, action, controller, null);
        }

        public static bool RouteMatch(this HtmlHelper helper, string action, string controller, RouteValueDictionary routeParam)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"] as string;
            var currentAction = routeData["action"] as string;

            bool actionMatch = string.Equals(action, currentAction, StringComparison.InvariantCultureIgnoreCase);
            bool controllerMatch = string.Equals(controller, currentController, StringComparison.InvariantCultureIgnoreCase);

            bool routeParamMatch;
            if (routeParam == null)
            {
                routeParamMatch = true;
            }
            else
            {
                var oneParam = routeParam.First();
                routeParamMatch = String.Equals(routeData[oneParam.Key] as string,
                                                oneParam.Value as string, StringComparison.OrdinalIgnoreCase);
            }

            return actionMatch && routeParamMatch && controllerMatch;
        }
        
        public static MvcHtmlString FormatDate (this HtmlHelper helper, DateTime date)
        {
            return new MvcHtmlString(date.ToShortDateString());
        }

        public static MvcHtmlString FormatNumber(this HtmlHelper helper, int number)
        {
            return new MvcHtmlString(number.ToString("N0"));
        }

        public static MvcHtmlString FormatNumber(this HtmlHelper helper, int? number)
        {
            if (number.HasValue)
            {
                return FormatNumber(helper, number.Value);
            }
            else
            {
                return new MvcHtmlString(string.Empty);
            }
        }
    }
}