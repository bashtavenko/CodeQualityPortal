using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;

using log4net;

namespace CodeQualityPortal.Common
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly ILog _log;

        public GlobalExceptionLogger(ILog log)
        {
            _log = log;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _log.Error(GetMethodAndUri(context.Request), context.Exception);
        }

        private static string GetMethodAndUri(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
            {
                message.Append(request.Method);
            }

            if (request.RequestUri != null)
            {
                message.Append(" ").Append(request.RequestUri);
            }
            return message.ToString();
        } 
    }
}