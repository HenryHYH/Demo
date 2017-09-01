using AutoMapper;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.WebApi.Models.Log;
using System;

namespace HelloWeb.MessageSystem.WebApi
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public static class MappingConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        public static void Register()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Exception, ExceptionMessage>()
                 .ForMember(d => d.FullName, o => o.MapFrom(s => s.GetType().FullName))
                 .ForMember(d => d.TargetSite, o => o.MapFrom(s => s.TargetSite.ToString()))
                 .ForMember(d => d.InnerMessage, o => o.MapFrom(s => Mapper.Map<Exception, ExceptionMessage>(s.InnerException)));
                c.CreateMap<LogModel, Log>()
                 .ForMember(d => d.Exception, o => o.MapFrom(s => s.Exception));
                c.CreateMap<Log, LogModel>();
            });
        }
    }
}