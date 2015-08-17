using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RecipeSearchBootstrap.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.prettyPhoto",
                      "~/Scripts/jqyery.isotope.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/wow.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/css/bootstrap.min.css",
                        "~/css/font-awesome.min.css",
                        "~/css/animate.min.css",
                        "~/css/prettyPhoto.css",
                        "~/css/main.css",
                        "~/css/responsive.css"));
        }
    }
}