﻿using Autofac;
using HelloWeb.MessageSystem.Core.Data;
using HelloWeb.MessageSystem.Core.Service;
using HelloWeb.MessageSystem.Core.Setting;
using HelloWeb.MessageSystem.MessageQueue;
using System;

namespace HelloWeb.MessageSystem.Core.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static IContainer Container { get; private set; }

        public static IContainer Register(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();

            builder.Register(x => SettingFactory.Create<SystemSetting>()).As<SystemSetting>().SingleInstance();
            builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            builder.Register(x => SettingFactory.Create<AliyunMnsSetting>()).As<AliyunMnsSetting>().SingleInstance();
            builder.RegisterGeneric(typeof(MqClient<>)).As(typeof(IQueue<>)).InstancePerLifetimeScope();

            action(builder);

            var container = builder.Build();
            Container = container;

            return container;
        }
    }
}
