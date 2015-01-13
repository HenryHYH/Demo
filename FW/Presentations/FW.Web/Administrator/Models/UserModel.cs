using FW.Web.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Models
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}