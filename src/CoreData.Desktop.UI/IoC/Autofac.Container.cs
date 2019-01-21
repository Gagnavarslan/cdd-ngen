#if Autofac
using Autofac;
using CoreData.Common.HostEnvironment;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;

namespace CoreData.Desktop.UI.IoC
{
    [ExcludeFromCodeCoverage]
    class Container<T> : IDisposable
    {
        private readonly IContainer _container;

        /// <summary>Builds container and invokes specified 'intializer' against ready container.</summary>
        internal Container() //Func<Container, T> with)
        {
            var builder = new ContainerBuilder();
            RegisterAppScopeServices(builder);
            RegisterSessionSingletons(builder);
            RegisterModules(builder);

            _container = builder.Build();

            //var root = with(_container);
            Root = _container.Resolve<T>();
            //_container.WithNoMoreRegistrationAllowed();
        }

        public T Root { get; }

        public void Dispose() => _container.Dispose();

        /// <summary>App scope initialization services regs. The most outer block, thus creating container builder.</summary>
        void RegisterAppScopeServices(ContainerBuilder builder)
        {
            // todo: implement and use with split into diff scopes: 1) init doesn't need all the stuff, even UI so will be quick
        }

        /// <summary>Shared through all session, but inner cmp to app scope initialization.</summary>
        void RegisterSessionSingletons(ContainerBuilder builder)
        {
            var tray = (TaskbarIcon)Application.Current.Resources["SysTray"];
            builder.RegisterInstance(tray);
            builder.RegisterInstance(new AppInfo(typeof(App).Assembly));
            builder.RegisterInstance(TaskScheduler.FromCurrentSynchronizationContext()); // UI Tasks Scheduler
        }

        /// <summary>Adds all modules regs to container as final point.</summary>
        void RegisterModules(ContainerBuilder builder)
        {
            builder
                .RegisterModule(new CommonModule())
                .RegisterModule(new DesktopCommonModule())
                .RegisterModule(new ServerModule())
                .RegisterModule(new FileSystemModule())
                .RegisterModule(new UIModule());
        }
    }
}
#endif