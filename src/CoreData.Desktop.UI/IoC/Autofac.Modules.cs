#if Autofac
using Autofac;
using CoreData.Common.HostEnvironment;
using CoreData.Desktop.UI.Tray;
using NLog;
using System.Diagnostics.CodeAnalysis;

namespace CoreData.Desktop.UI.IoC
{
    [ExcludeFromCodeCoverage]
    class CommonModule : Module
    {
        // https://autofac.readthedocs.io/en/latest/register/registration.html#open-generic-components
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ILogger>(ctx => LogManager.GetLogger(ctx.GetType().Name));
            builder.RegisterInstance(new EnvInfo());

            builder.RegisterGeneric(typeof(CommandPromptShell)).As(typeof(IShell<>)).SingleInstance();
            builder.RegisterType<WebBrowser>().As<IWebBrowser>().SingleInstance();
        }
    }

    [ExcludeFromCodeCoverage]
    class DesktopCommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Cmd>().As<ICmd>().SingleInstance();
        }
    }

    [ExcludeFromCodeCoverage]
    class ServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Cmd>().As<ICmd>().SingleInstance();
        }
    }
    
    [ExcludeFromCodeCoverage]
    class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Cmd>().As<ICmd>().SingleInstance();
        }
    }

    [ExcludeFromCodeCoverage]
    class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppTrayData>().AsSelf().SingleInstance();
            builder.RegisterType<AppTrayView>().AsSelf().SingleInstance();
            //#if DEBUG
            //            container.RegisterInitializer<object>((service, resolver) =>
            //                Trace.TraceWarning($"RESOLVED obj: {service.GetType()}"));
            //#endif

            //            container.Register<ICoreDataConnection, CoreDataConnection>(Reuse.Singleton);
        }
    }
}
#endif