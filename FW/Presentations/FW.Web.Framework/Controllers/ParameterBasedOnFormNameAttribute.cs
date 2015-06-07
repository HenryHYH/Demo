using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.Web.Framework.Controllers
{
    /// <summary>
    /// 如果表单提交的 Name 存在，则对应的 ActionParameterName 设置为 true
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterBasedOnFormNameAttribute : FilterAttribute, IActionFilter
    {
        private readonly string name;
        private readonly string actionParameterName;

        public ParameterBasedOnFormNameAttribute()
        {
            this.name = "save-continue";
            this.actionParameterName = "continueEditing";
        }

        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            this.name = name;
            this.actionParameterName = actionParameterName;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var formValue = filterContext.RequestContext.HttpContext.Request.Form[name];
            filterContext.ActionParameters[actionParameterName] = !string.IsNullOrEmpty(formValue);
        }
    }
}
