using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UPCHINA.MultiTenancy.Dto;

namespace UPCHINA.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultOutput<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
