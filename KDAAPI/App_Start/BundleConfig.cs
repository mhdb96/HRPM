using System.Web;
using System.Web.Optimization;

namespace KDAAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //  "~/Content/themes/base/jquery.ui.core.css",
            //  "~/Content/themes/base/jquery.ui.resizable.css",
            //  "~/Content/themes/base/jquery.ui.selectable.css",
            //  "~/Content/themes/base/jquery.ui.accordion.css",
            //  "~/Content/themes/base/jquery.ui.autocomplete.css",
            //  "~/Content/themes/base/jquery.ui.button.css",
            //  "~/Content/themes/base/jquery.ui.dialog.css",
            //  "~/Content/themes/base/jquery.ui.slider.css",
            //  "~/Content/themes/base/jquery.ui.tabs.css",
            //  "~/Content/themes/base/jquery.ui.datepicker.css",
            //  "~/Content/themes/base/jquery.ui.progressbar.css",
            //  "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/MD").Include(
              "~/Content/MD/mdb.css"));
            //"~/Content/MD/mdb.min.css",
            //"~/Content/MD/mdb.lite.css",
            //"~/Content/MD/addons/datatables-select.min.css",
            //"~/Content/MD/addons/datatables-select2.min.css",
            //"~/Content/MD/addons/datatables.min.css",
            //"~/Content/MD/addons/datatables2.min.css"

            bundles.Add(new ScriptBundle("~/bundles/MD").Include(
            "~/Scripts/MD/mdb.js"));
            //"~/Scripts/MD/mdb.min.js",
            //"~/Scripts/MDbootstrap.js",
            //"~/Scripts/MD/bootstrap.min.js",
            //"~/Scripts/MD/jquery.js",
            //"~/Scripts/MD/jquery.min.js",
            //"~/Scripts/MD/popper.js",
            //"~/Scripts/MD/popper.min.js",
            //"~/Scripts/MD/addons/datatables-select.min.js",
            //"~/Scripts/MD/addons/datatables-select2.min.js",
            //"~/Scripts/MD/addons/datatables.min.js",
            //"~/Scripts/MD/addons/datatables2.min.js",
            //"~/Scripts/MD/addons/directives.js",
            //"~/Scripts/MD/addons/flag.min.js",
            //"~/Scripts/MD/addons/imagesloaded.pkgd.min.js",
            //"~/Scripts/MD/addons/jquery.zmd.hierarchical-display.min.js",
            //"~/Scripts/MD/addons/masonry.pkgd.min.js",
            //"~/Scripts/MD/addons/rating.min.js",
            //"~/Scripts/MD/modules/animations-extended.min.js",
            //"~/Scripts/MD/modules/forms-free.min.js",
            //"~/Scripts/MD/modules/scrolling-navbar.min.js",
            //"~/Scripts/MD/modules/treeview.min.js",
            //"~/Scripts/MD/modules/wow.min.js"


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/popper.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
