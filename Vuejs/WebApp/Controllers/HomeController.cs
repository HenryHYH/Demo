using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private static readonly IList<Todo> TODOS = new List<Todo>() { 
                                                        new Todo() { Text = "Hello world" }, 
                                                        new Todo() { Text = "Ajax" }
                                                    };

        #endregion

        #region Page

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Ajax

        [HttpGet]
        public ActionResult GetAjax()
        {
            return Json(TODOS, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddAjax(string todo)
        {
            TODOS.Add(new Todo() { Text = todo });

            return Json(true);
        }

        #endregion
    }
}