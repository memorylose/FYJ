using System.Web.Optimization;

namespace FYJ.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Script/jquery-1.11.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Script/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/logincss").Include(
                      "~/Style/bootstrap.min.css",
                      "~/Style/Login.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}