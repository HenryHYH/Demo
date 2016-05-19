using Abp.Authorization;
using Demo.Authorization.Roles;
using Demo.MultiTenancy;
using Demo.Users;

namespace Demo.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
