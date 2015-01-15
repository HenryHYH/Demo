namespace FW.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class NavigationModel
    {
        #region Constructors

        public NavigationModel()
        {
            Children = new List<NavigationModel>();
        }

        #endregion Constructors

        #region Properties

        public IList<NavigationModel> Children
        {
            get; private set;
        }

        public string Title
        {
            get; set;
        }

        public string Url
        {
            get; set;
        }

        #endregion Properties
    }
}