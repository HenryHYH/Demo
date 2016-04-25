using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Infrastructure
{
    public class ServerConfig : ConfigurationSection
    {
        [ConfigurationProperty("address")]
        public UriSpecification Address
        {
            get { return this["address"] as UriSpecification; }
            set { this["address"] = value; }
        }

        public static ServerConfig GetConfiguration()
        {
            var config = ConfigurationManager.GetSection("serverConfig") as ServerConfig;

            return config ?? new ServerConfig();
        }
    }

    public class UriSpecification : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public Uri Uri
        {
            get { return this["uri"] as Uri; }
            set { this["uri"] = value; }
        }
    }
}
