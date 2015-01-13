using AutoMapper;
using FW.Admin.Models;
using FW.Core;
using FW.Core.Domain.Localization;
using FW.Core.Domain.Users;
using FW.Core.Infrastructure;
using FW.Web.Framework.UI.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Admin.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<UserModel, User>();

            Mapper.CreateMap<LocalizedResource, LocalizedResourceModel>();
            Mapper.CreateMap<LocalizedResourceModel, LocalizedResource>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}