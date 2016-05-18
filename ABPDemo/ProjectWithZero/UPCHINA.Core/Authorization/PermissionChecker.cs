using Abp.Authorization;
using UPCHINA.Authorization.Roles;
using UPCHINA.MultiTenancy;
using UPCHINA.Users;

namespace UPCHINA.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
