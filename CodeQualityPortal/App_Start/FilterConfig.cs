using System.Web.Mvc;

using log4net;

using CodeQualityPortal.Common;
using CodeQualityPortal.Controllers;

namespace CodeQualityPortal
{
    public class FilterConfig
    {
        private static ILog _log;

        static FilterConfig()
        {
            _log = LogManager.GetLogger(typeof(HomeController));
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionFilter(_log, string.Empty, "Error"));
        }
    }
}
