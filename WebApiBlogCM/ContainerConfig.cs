using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiBlogCM.Models;
using WebApiBlogCM.Data;
using System.Reflection;

namespace WebApiBlogCM
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Post>().As<IPost>();
            builder.RegisterType<PostResponse>().As<IPostResponse>();
            builder.RegisterType<DataAccess>().As<IDataAccess>();
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(WebApiBlogCM)))
            //   .Where(t => t.Namespace.Contains("Models") || t.Namespace.Contains("Data"))
            //   .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
            return builder.Build();
        }
    }
}