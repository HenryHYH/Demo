using System;
using System.Configuration;

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
