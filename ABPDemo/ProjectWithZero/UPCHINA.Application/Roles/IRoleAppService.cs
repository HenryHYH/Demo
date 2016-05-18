using System.Threading.Tasks;
using Abp.Application.Services;
using UPCHINA.Roles.Dto;

namespace UPCHINA.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
