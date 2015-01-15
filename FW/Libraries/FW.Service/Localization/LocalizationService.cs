using FW.Core;
using FW.Core.Caching;
using FW.Core.Data;
using FW.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Service.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private const string RESOURCE_CACHE_KEY = "LOCALIZED_RESOURCE.{0}_{1}";
        private const string RESOURCE_CACHE_ID = "LOCALIZED_RESOURCE.{0}";
        private const string RESOURCE_CACHE_PATTERN = "LOCALIZED_RESOURCE.";

        private const string LANGUAGE_CACHE_ID = "LANGUAGE.{0}";
        private const string LANGUAGE_CACHE_PATTERN = "LANGUAGE.";

        private readonly IRepository<LocalizedResource> localizedResourceRepository;
        private readonly IRepository<Language> languageRepository;
        private readonly ICacheManager cacheManager;

        public LocalizationService(IRepository<LocalizedResource> localizedResourceRepository,
            IRepository<Language> languageRepository,
            ICacheManager cacheManger)
        {
            this.localizedResourceRepository = localizedResourceRepository;
            this.languageRepository = languageRepository;
            this.cacheManager = cacheManger;
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
                            x.Language == language)
                    .Select(x => x.ResourceValue)
                    .FirstOrDefault();

                return resource;
            });
        }

        public void InsertResource(LocalizedResource resource)
        {
            localizedResourceRepository.Insert(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        public void UpdateResource(LocalizedResource resource)
        {
            localizedResourceRepository.Update(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }

        public void DeleteResource(LocalizedResource resource)
        {
            localizedResourceRepository.Delete(resource);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }


        public IPagedList<LocalizedResource> GetResources(
            int pageIndex = 1,
            int pageSize = 20)
        {
            var query = localizedResourceRepository.Table;

            return new PagedList<LocalizedResource>(query, pageIndex, pageSize);
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


        public IPagedList<Language> GetLanguages(int pageIndex = 1, int pageSize = 20)
        {
            var query = languageRepository.Table;

            return new PagedList<Language>(query, pageIndex, pageSize);
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

        public void InsertLanguage(Language entity)
        {
            languageRepository.Insert(entity);
            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
        }

        public void UpdateLanguage(Language entity)
        {
            languageRepository.Update(entity);
            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
        }

        public void DeleteLanguage(Language entity)
        {
            localizedResourceRepository.Delete(x => x.Language == entity.Id);
            languageRepository.Delete(entity);

            cacheManager.RemoveByPattern(LANGUAGE_CACHE_PATTERN);
            cacheManager.RemoveByPattern(RESOURCE_CACHE_PATTERN);
        }
    }
}
