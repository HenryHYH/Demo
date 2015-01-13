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

        private readonly IRepository<LocalizedResource> localizedResourceRepository;
        private readonly ICacheManager cacheManager;

        public LocalizationService(IRepository<LocalizedResource> localizedResourceRepository,
            ICacheManager cacheManger)
        {
            this.localizedResourceRepository = localizedResourceRepository;
            this.cacheManager = cacheManger;
        }

        public string GetResource(string key)
        {
            return GetResource(key, "CN");
        }

        public string GetResource(string key, string language)
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
    }
}
