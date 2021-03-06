﻿using System.Web;
using System.Web.Optimization;

namespace Manulife.TopFiveWebsites.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/jquery.number.js",
                      "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/search-listtopwebsites").Include(
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/search.listtopwebsites.js"));

            bundles.Add(new StyleBundle("~/Content/search-listtopwebsites").Include(
                      "~/Content/bootstrap-datetimepicker.css"));

            //bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            //    "~/Scripts/datatables.js"));

            //bundles.Add(new StyleBundle("~/Content/css/datatables").Include(
            //    "~/Content/datatables.css"));
        }
    }
}
