﻿using System.Web;
using System.Web.Optimization;

namespace LibraryManageSys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.6.0.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-notify.min.js",
                      "~/Scripts/custom.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                    "~/Scripts/perfect-scrollbar.min.js",
                    "~/Scripts/lightyear.js",
                    "~/Scripts/main.min.js"
          ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/materialdesignicons.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/custom.css",
                      "~/Content/style.min.css"
                      ));
        }
    }
}
