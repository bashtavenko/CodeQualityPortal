using System;
using System.Web;
using System.Web.Mvc;

using log4net;

namespace CodeQualityPortal.Common
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly string _view;
        private readonly string _master;
        private readonly ILog _log;

        public GlobalExceptionFilter(ILog loggingService, string master, string view)
        {
            _log = loggingService;
            _master = master ?? string.Empty;
            _view = view ?? string.Empty; 
        }

        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.IsChildAction && (!filterContext.ExceptionHandled
                && filterContext.HttpContext.IsCustomErrorEnabled))
            {
                Exception exception = filterContext.Exception;
                if ((new HttpException(null, exception).GetHttpCode() == 500))
                {
                    // Log exception
                    try
                    {
                        _log.Error(exception);
                    }
                    catch (Exception ex)
                    {
                        exception.Data.Add("Loging failed", ex.Message);
                    }

                    // Show error view
                    ErrorFilterHelper.SetFilerContext(filterContext, _master, _view);
                }
            }
        }
    }
}