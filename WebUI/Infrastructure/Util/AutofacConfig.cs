using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using System.Web;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Enities;
using System.Web.Http;
using System.Configuration;
using System.Reflection;
using Domain.EF;

namespace WebUI.Infrastructure.Util
{
    public class AutofacConfig
    {
        public static void ConfigurationContainer()
        {
            var builder = new ContainerBuilder();
            
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<EFDbContext>().As<EFDbContext>();

            builder.RegisterType<Domain.EF.AppContext>().As<Domain.EF.AppContext>();

            builder.RegisterType<EmailOrderProcessor>().As<IOrderProcessor>().WithParameter("settings", emailSettings);

            builder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IGenericRepository<>));

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

        }
    }
}