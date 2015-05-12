﻿using FW.Core;
using FW.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new CustomException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new CustomException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
