namespace FW.Admin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using FW.Admin.Models;
    using FW.Core;
    using FW.Core.Domain.Localization;
    using FW.Core.Domain.Users;
    using FW.Core.Infrastructure;
    using FW.Web.Framework.UI.Pagination;
    using FW.Service.Localization;

    public class AutoMapperStartupTask : IStartupTask
    {
        #region Properties

        public int Order
        {
            get { return 0; }
        }

        #endregion Properties

        #region Methods

        public void Execute()
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<UserModel, User>();

            Mapper.CreateMap<LocalizedResource, LocalizedResourceModel>()
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(source => localizationService.GetLanguage(source.LanguageId).Name));
            Mapper.CreateMap<LocalizedResourceModel, LocalizedResource>();

            Mapper.CreateMap<Language, LanguageModel>();
            Mapper.CreateMap<LanguageModel, Language>();
        }

        #endregion Methods
    }
}