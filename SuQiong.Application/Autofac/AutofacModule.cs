using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SuQiong.Application
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //反射程序集方式服务注册
            builder.RegisterAssemblyTypes(Assembly.Load("SuQiong.Service"), Assembly.Load("SuQiong.Domain.Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.Load("SuQiong.Repository"), Assembly.Load("SuQiong.Domain.Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
