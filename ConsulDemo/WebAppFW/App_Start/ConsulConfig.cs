﻿using Consul;
using System;

namespace WebAppFW
{
    public partial class ConsulConfig
    {
        public static void RegisterConsul()
        {
            var consulAddress = "http://192.168.5.230:8500/";
            using (var client = new ConsulClient(config => config.Address = new Uri(consulAddress)))
            {
                var uri = new Uri("http://localhost:50992/");
                var registration = new AgentServiceRegistration
                {
                    ID = $"WebAppA-{uri.Port}",
                    Name = "Name:WebAppA",
                    Address = $"{uri.Host}",
                    Port = uri.Port,
                    Tags = new[] { "A", "B", "C" }
                };

                client.Agent.ServiceDeregister(registration.ID).Wait();
                client.Agent.ServiceRegister(registration).Wait();
            }
        }
    }
}