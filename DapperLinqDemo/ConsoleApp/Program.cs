using System;
using Castle.Windsor;
using ConsoleApp.Domains;
using Dapper.Extensions.Linq.CastleWindsor;
using Dapper.Extensions.Linq.Core.Configuration;
using Dapper.Extensions.Linq.Core.Repositories;
using Dapper.Extensions.Linq.Mapper;
using Dapper.Extensions.Linq.Sql;

namespace ConsoleApp
{
    class Program
    {
        private static WindsorContainer _container = new WindsorContainer();

        static void Main(string[] args)
        {
            Init();
            GetById();
        }

        private static void GetById()
        {
            var personRepository = _container.Resolve<IRepository<User>>();

            User p1 = new User { Name = "Test", Age = 100, Enabled = true };
            int id = personRepository.Insert(p1);

            User p2 = personRepository.Get(id);
            Console.WriteLine("Id = " + p2.Id);
            Console.WriteLine("Name = " + p2.Name);
            Console.WriteLine("Age = " + p2.Age);
        }

        private static void Init()
        {
            DapperConfiguration
                .Use()
                .UseClassMapper(typeof(AutoClassMapper<>))
                .UseContainer<ContainerForWindsor>(cfg => cfg.UseExisting(_container))
                .UseSqlDialect(new SqlServerDialect())
                .WithDefaultConnectionStringNamed("__DefaultSqlServer")
                .FromAssembly("ConsoleApp.Domains")
                .FromAssembly("ConsoleApp.Maps")
                .Build();
        }
    }
}
