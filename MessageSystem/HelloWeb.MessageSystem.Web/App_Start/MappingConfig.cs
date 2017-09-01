using AutoMapper;
using HelloWeb.MessageSystem.Core.Domain.Logging;
using HelloWeb.MessageSystem.WebApi.Models.Log;

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
                c.CreateMap<LogModel, Log>();
                c.CreateMap<Log, LogModel>();
            });
        }
    }
}