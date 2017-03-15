using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;
using Services.Repository;
using Services;

namespace CtorInject
{
    public static class UnityConfig
    {
        public static IUnityContainer Current { get; private set; }

        static UnityConfig()
        {
            Current = new UnityContainer();
            Current.LoadConfiguration();
        }
    }
}
