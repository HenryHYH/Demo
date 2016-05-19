using System.Threading.Tasks;
using Abp.Application.Services;
using Demo.Roles.Dto;

namespace Demo.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
