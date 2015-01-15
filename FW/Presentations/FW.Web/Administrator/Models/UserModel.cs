namespace FW.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using FW.Web.Framework.MVC;

    public class UserModel : BaseModel
    {
        #region Properties

        public int Age
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}