using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Exceptionless;
using NWebsec.Csp;

namespace CSPLogger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void NWebsecHttpHeaderSecurityModule_CspViolationReported(object sender, CspViolationReportEventArgs e)
        {
            var report = e.ViolationReport;
            var directive = report.Details.ViolatedDirective.Split(' ').FirstOrDefault();

            ExceptionlessClient.Default.CreateLog($"ContentSecurityPolicy:{directive}",
                $"Violation:{report.Details.BlockedUri}", Exceptionless.Logging.LogLevel.Warn)
                 .AddObject(report.Details)
                 .Submit();
        }
    }
}
