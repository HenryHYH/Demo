using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using UPCHINA.Authorization;
using UPCHINA.MultiTenancy;

namespace UPCHINA.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : UPCHINAControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}