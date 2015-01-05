using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Models.Common
{
    public class NavigationModel
    {
        public NavigationModel()
        {
            Children = new List<NavigationModel>();
        }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IList<NavigationModel> Children { get; set; }
    }
}