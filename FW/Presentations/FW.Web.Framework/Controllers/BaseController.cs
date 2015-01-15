namespace FW.Web.Framework.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using FW.Core.Infrastructure;
    using FW.Service.Logging;

    public abstract class BaseController : Controller
    {
        #region Methods

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (null != filterContext.Exception)
                LogException(filterContext.Exception);

            base.OnException(filterContext);
        }

        private void LogException(Exception ex)
        {
            var logger = EngineContext.Current.Resolve<ILogger>();
            logger.Error(ex.Message, ex);
        }

        #endregion Methods
    }
}