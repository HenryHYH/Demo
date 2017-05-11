using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace EntlibDemo
{
    public class EntlibConfig
    {
        public static void Configuration()
        {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            PolicyInjector policyInjector = new PolicyInjector(configurationSource);
            PolicyInjection.SetPolicyInjector(policyInjector);
        }
    }
}
