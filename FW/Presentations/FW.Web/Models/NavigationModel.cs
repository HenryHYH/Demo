using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Web.Models
{
    public class NavigationModel
    {
        public NavigationModel()
        {
            Children = new List<NavigationModel>();
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public IList<NavigationModel> Children { get; private set; }
    }
}