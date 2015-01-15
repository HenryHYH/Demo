namespace FW.Service.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core;
    using FW.Core.Domain.Localization;

    public interface ILocalizationService
    {
        #region Methods

        void DeleteLanguage(Language entity);

        void DeleteResource(LocalizedResource resource);

        Language GetLanguage(int id);

        IPagedList<Language> GetLanguages(int pageIndex = 1, int pageSize = 20);

        LocalizedResource GetResource(int id);

        string GetResource(string key);

        string GetResource(string key, int language);

        IPagedList<LocalizedResource> GetResources(int languageId, int pageIndex = 1, int pageSize = 20);

        void InsertLanguage(Language entity);

        void InsertResource(LocalizedResource resource);

        void UpdateLanguage(Language entity);

        void UpdateResource(LocalizedResource resource);

        #endregion Methods
    }
}