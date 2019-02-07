#if !Autofac
using System;
using System.Diagnostics.CodeAnalysis;
using CoreData.Common.HostEnvironment;
using CoreData.Common.Settings;
using CoreData.Desktop.UI.Tray;
using DryIoc;
using NLog;

namespace CoreData.Desktop.UI.IoC
{
    [ExcludeFromCodeCoverage]
    static class CommonModule
    {
        internal static void Register(DryIoc.Container container)
        {
            Type getImplType(Request r) => r.Parent.ImplementationType;

            // https://bitbucket.org/dadhi/dryioc/wiki/ExamplesContextBasedResolution
            container.Register(
                made: Made.Of(() => LogManager.GetCurrentClassLogger(Arg.Index<Type>(0)), getImplType),
                setup: Setup.With(condition: req => req.Parent.ImplementationType != null),
                reuse: Reuse.Transient);

            container.UseInstance(new EnvInfo());

            container.Register<ISettingsService, SettingsService>(Reuse.Singleton);
            container.Register(typeof(IShell<>), typeof(CommandPromptShell), Reuse.Singleton); // reg open gen https://bitbucket.org/dadhi/dryioc/wiki/OpenGenerics 
            container.Register<IWebBrowser, WebBrowser>(Reuse.Singleton);
        }
    }

    [ExcludeFromCodeCoverage]
    static class DesktopCommonModule
    {
        internal static void Register(DryIoc.Container container)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    static class ServerModule
    {
        internal static void Register(DryIoc.Container container)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    static class FileSystemModule
    {
        internal static void Register(DryIoc.Container container)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    static class UIModule
    {
        internal static void Register(DryIoc.Container container)
        {
            container.Register<AppTrayViewModel>(Reuse.Singleton);
            container.Register<AppTrayView>(Reuse.Singleton);
            //#if DEBUG
            //            container.RegisterInitializer<object>((service, resolver) =>
            //                Trace.TraceWarning($"RESOLVED obj: {service.GetType()}"));
            //#endif

            // container.Register<ICoreDataConnection, CoreDataConnection>(Reuse.Singleton);
        }
    }
}
#endif