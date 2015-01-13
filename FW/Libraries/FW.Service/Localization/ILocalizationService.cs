using FW.Core;
using FW.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Service.Localization
{
    public interface ILocalizationService
    {
        LocalizedResource GetResource(int id);

        string GetResource(string key);

        string GetResource(string key, string language);

        IPagedList<LocalizedResource> GetResources(int pageIndex = 1, int pageSize = 20);

        void InsertResource(LocalizedResource resource);

        void UpdateResource(LocalizedResource resource);

        void DeleteResource(LocalizedResource resource);
    }
}
