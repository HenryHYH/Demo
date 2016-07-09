using Microsoft.Owin.StaticFiles.ContentTypes;

namespace ConsoleApp
{
    public class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        #region Ctor

        public CustomContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }

        #endregion
    }
}