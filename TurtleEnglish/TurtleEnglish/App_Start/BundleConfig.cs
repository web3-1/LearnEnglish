using System.Web;
using System.Web.Optimization;

namespace TurtleEnglish
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            
            //kha add admin
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Style/AdminPageStyle/vendors/bootstrap/dist/css/bootstrap.min.css",
                        "~/Style/AdminPageStyle/vendors/font-awesome/css/font-awesome.min.css",
                        "~/Style/AdminPageStyle/vendors/nprogress/nprogress.css",
                        "~/Style/AdminPageStyle/vendors/bootstrap-daterangepicker/daterangepicker.css",
                        "~/Style/AdminPageStyle/build/css/custom.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Style/AdminPageStyle/vendors/jquery/dist/jquery.min.js",
                        "~/Style/AdminPageStyle/vendors/bootstrap/dist/js/bootstrap.min.js",
                        "~/Style/AdminPageStyle/vendors/fastclick/lib/fastclick.js",
                        "~/Style/AdminPageStyle/vendors/nprogress/nprogress.js",
                        "~/Style/AdminPageStyle/vendors/Chart.js/dist/Chart.min.js",
                        "~/Style/AdminPageStyle/vendors/jquery-sparkline/dist/jquery.sparkline.min.js",
                        "~/Style/AdminPageStyle/vendors/Flot/jquery.flot.js",
                        "~/Style/AdminPageStyle/vendors/Flot/jquery.flot.pie.js",
                        "~/Style/AdminPageStyle/vendors/Flot/jquery.flot.time.js",
                        "~/Style/AdminPageStyle/vendors/Flot/jquery.flot.stack.js",
                        "~/Style/AdminPageStyle/vendors/Flot/jquery.flot.resize.js",
                        "~/Style/AdminPageStyle/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                        "~/Style/AdminPageStyle/vendors/flot-spline/js/jquery.flot.spline.min.js",
                        "~/Style/AdminPageStyle/vendors/flot.curvedlines/curvedLines.js",
                        "~/Style/AdminPageStyle/vendors/DateJS/build/date.js",
                        "~/Style/AdminPageStyle/vendors/moment/min/moment.min.js",
                        "~/Style/AdminPageStyle/vendors/bootstrap-daterangepicker/daterangepicker.js",
                        "~/Style/AdminPageStyle/build/js/custom.min.js"
            ));
        }
    }
}
