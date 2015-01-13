using FW.Web.Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Models
{
    public class LocalizedResourceModel : BaseModel
    {
        public string Language { get; set; }

        public string Key { get; set; }

        public string Resource { get; set; }
    }
}