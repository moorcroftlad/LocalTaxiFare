﻿using System.Web.Optimization;

namespace LocalTaxiFare.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/assets/js/bootstrap.js",
                "~/assets/js/jquery-1.9.1.js",
                "~/assets/js/autocomplete.js",
                "~/assets/js/searchForm.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/bootstrap-responsive.css",
                "~/assets/css/style.css"));
        }
    }
}