using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UPCHINA.MultiTenancy;

namespace UPCHINA.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}