using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Dapper.Extensions.Linq.CastleWindsor;
using Dapper.Extensions.Linq.Core.Configuration;
using Dapper.Extensions.Linq.Mapper;
using Dapper.Extensions.Linq.Sql;

namespace ConsoleApp.Tests
{
    public abstract class BaseTest
    {
        public static readonly WindsorContainer CONTAINER = new WindsorContainer();

        public BaseTest()
        {
            DapperConfiguration
                .Use()
                .UseClassMapper(typeof(AutoClassMapper<>))
                .UseContainer<ContainerForWindsor>(cfg => cfg.UseExisting(CONTAINER))
                .UseSqlDialect(new SqlServerDialect())
                .WithDefaultConnectionStringNamed("__DefaultSqlServer")
                .FromAssembly("ConsoleApp.Domains")
                .FromAssembly("ConsoleApp.Maps")
                .Build();
        }
    }
}
