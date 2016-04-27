using System.Reflection;
using System.Web.Http.Filters;
using log4net;

namespace MS.Framework
{
    public class LoggingFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            logger.Error(actionExecutedContext.Exception);

            base.OnException(actionExecutedContext);
        }
    }
}
