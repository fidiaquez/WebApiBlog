using Autofac;
using Autofac.Integration.WebApi;
//using AutoFacWithWebAPI.Repository;
//using AutoFacWithWebAPI.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using WebApiBlogCM.Models;
using WebApiBlogCM.Data;


namespace WebApiBlogCM.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<DBCustomerEntities>()
            //       .As<DbContext>()
            //       .InstancePerRequest();
            builder.RegisterType<Post>().As<IPost>().InstancePerRequest();
            builder.RegisterType<Response>().As<IResponse>().InstancePerRequest();
            builder.RegisterType<PostResponse>().As<IPostResponse>().InstancePerRequest();
            builder.RegisterType<DataAccess>().As<IDataAccess>().InstancePerRequest();

            //builder.RegisterType<>()
            //       .As<IDbFactory>()
            //       .InstancePerRequest();

            //builder.RegisterGeneric(typeof( <>))
            //       .As(typeof(IPo<>))
            //       .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }

}