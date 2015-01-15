namespace FW.Service.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FW.Core;
    using FW.Core.Caching;
    using FW.Core.Data;
    using FW.Core.Domain.Localization;

    public class LocalizationService : ILocalizationService
    {
        #region Fields

        private const string LANGUAGE_CACHE_ID = "LANGUAGE.{0}";
        private const string LANGUAGE_CACHE_PATTERN = "LANGUAGE.";
        private const string RESOURCE_CACHE_ID = "LOCALIZED_RESOURCE.{0}";
        private const string RESOURCE_CACHE_KEY = "LOCALIZED_RESOURCE.{0}_{1}";
        private const string RESOURCE_CACHE_PATTERN = "LOCALIZED_RESOURCE.";

        private readonly ICacheManager cacheManager;
        private readonly IRepository<Language> languageRepository;
        private readonly IRepository<LocalizedResource> localizedResourceRepository;

        #endregion Fields

        #region Constructors

        public LocalizationService(IRepository<LocalizedResource> localizedResourceRepository,
            IRepository<Language> languageRepository,
            ICacheManager cacheManger)
        {
            this.localizedResourceRepository = localizedResourceRepository;
            this.languageRepository = languageRepository;
            this.cacheManager = cacheManger;
        }

        #endregion Constructors

        #region Methods

        public void DeleteLanguage(Language entity)
        {
            localizedResourceRepository.Delete(x => x.LanguageId == entity.Id);
            languageRepository.Delete(entity);

            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        public void DeleteResource(LocalizedResource resource)
        {
            localizedResourceRepository.Delete(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        public Language GetLanguage(int id)
        {
            if (0 == id)
                return null;

            return cacheManager.Get(
                string.Format(LANGUAGE_CACHE_ID, id),
                () => languageRepository.Table
                .Where(x => x.Id == id)
                .FirstOrDefault());
        }

        public IPagedList<Language> GetLanguages(int pageIndex = 1, int pageSize = 20)
        {
            var query = languageRepository.Table;

            return new PagedList<Language>(query, pageIndex, pageSize);
        }

        public string GetResource(string key)
        {
            return GetResource(key, 1);
        }

        public string GetResource(string key, int language)
        {
            return cacheManager.Get(string.Format(RESOURCE_CACHE_KEY, key, language), () =>
            {
                var resource = localizedResourceRepository.Table
                    .Where(x => x.ResourceKey == key &&
                            x.LanguageId == language)
                    .Select(x => x.ResourceValue)
                    .FirstOrDefault();

                return resource;
            });
        }

        public LocalizedResource GetResource(int id)
        {
            if (0 == id)
                return null;

            return cacheManager.Get(
                string.Format(RESOURCE_CACHE_ID, id),
                () => localizedResourceRepository.Table
                    .Where(x => x.Id == id)
                    .FirstOrDefault());
        }

        public IPagedList<LocalizedResource> GetResources(int languageId,
            int pageIndex = 1,
            int pageSize = 20)
        {
            var query = localizedResourceRepository.Table
                                                    .Where(x => x.LanguageId == languageId);

            return new PagedList<LocalizedResource>(query, pageIndex, pageSize);
        }

        public void InsertLanguage(Language entity)
        {
            languageRepository.Insert(entity);
            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
        }

        public void InsertResource(LocalizedResource resource)
        {
            localizedResourceRepository.Insert(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        public void UpdateLanguage(Language entity)
        {
            languageRepository.Update(entity);
            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
        }

        public void UpdateResource(LocalizedResource resource)
        {
            localizedResourceRepository.Update(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        #endregion Methods
    }
}