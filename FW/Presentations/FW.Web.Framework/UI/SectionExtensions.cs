namespace FW.Web.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.WebPages;

    public static class SectionExtensions
    {
        #region Fields

        private static readonly object _o = new object();

        #endregion Fields

        #region Methods

        public static HelperResult RedefineSection(this WebPageBase page,
            string sectionName)
        {
            return RedefineSection(page, sectionName, defaultContent: null);
        }

        public static HelperResult RedefineSection(this WebPageBase page,
            string sectionName,
            Func<object, HelperResult> defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                page.DefineSection(sectionName,
                                    () => page.Write(page.RenderSection(sectionName)));
            }
            else if (defaultContent != null)
            {
                page.DefineSection(sectionName,
                                    () => page.Write(defaultContent(_o)));
            }
            return new HelperResult(_ => { });
        }

        public static HelperResult RenderSection(this WebPageBase page,
            string sectionName,
            Func<object, HelperResult> defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return page.RenderSection(sectionName);
            }
            else
            {
                return defaultContent(_o);
            }
        }

        #endregion Methods
    }
}