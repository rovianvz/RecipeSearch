using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeSearchBootstrap.Utilities
{
    public static class Utilities
    {
        public static string IsActive(this HtmlHelper html,
                                  string control,
                                  string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control == routeControl &&
                               action == routeAction;

            return returnActive ? "active" : "";
        }

        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controllerName, string imagePath, string alt, string anchorCssClass)
        {
            return ActionImage(html, action, controllerName, imagePath, alt, anchorCssClass, null, null);
        }
        
        // Extension method
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controllerName, string imagePath, string alt, string anchorCssClass, string imgCssClass)
        {
            return ActionImage(html, action, controllerName, imagePath, alt, anchorCssClass, imgCssClass, null);
        }
        // Extension method
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controllerName, string imagePath, string alt, string anchorCssClass, string imgCssClass, object routeAttributes)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            if(imgCssClass != null)
                imgBuilder.AddCssClass(imgCssClass);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            if (routeAttributes == null)
                anchorBuilder.MergeAttribute("href", url.Action(action, controllerName));
            else
                anchorBuilder.MergeAttribute("href", url.Action(action, controllerName, routeAttributes));
            
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            if(anchorCssClass != null)
                anchorBuilder.AddCssClass(anchorCssClass);
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}