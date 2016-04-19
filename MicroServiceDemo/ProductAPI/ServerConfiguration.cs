using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI
{
    public class ServerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("address")]
        public UriSpecification Address
        {
            get { return this["address"] as UriSpecification; }
            set { this["address"] = value; }
        }

        public static ServerConfiguration GetConfiguration()
        {
            var config = ConfigurationManager.GetSection("serverConfiguration") as ServerConfiguration;

            return config ?? new ServerConfiguration();
        }
    }

    public class UriSpecification : ConfigurationElement
    {
        [ConfigurationProperty("uri", DefaultValue = "http://localhost:8802/", IsRequired = true)]
        public Uri Uri
        {
            get { return this["uri"] as Uri; }
            set { this["uri"] = value; }
        }
    }
}
