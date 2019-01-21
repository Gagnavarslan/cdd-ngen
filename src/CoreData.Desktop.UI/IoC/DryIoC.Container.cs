#if !Autofac
using CoreData.Common.HostEnvironment;
using DryIoc;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;

namespace CoreData.Desktop.UI.IoC
{
    [ExcludeFromCodeCoverage]
    class Container : IDisposable
    {
        private readonly DryIoc.Container _container;

        /// <summary>Builds container and invokes specified 'intializer' against ready container.</summary>
        internal Container() // (Func<Container, T> with)
        {
            _container = new DryIoc.Container();

            RegisterAppScopeServices(_container);
            RegisterSessionSingletons(_container);
            RegisterModules(_container);
            //var root = with(_container);
            //var root = container.Resolve<T>();
            //_container.WithNoMoreRegistrationAllowed();
            //return (container, root);
        }

        internal T Resolve<T>() => _container.Resolve<T>();

        public void Dispose() => _container.Dispose();

        /// <summary>App scope initialization services regs. The most outer block, thus creating container builder.</summary>
        static void RegisterAppScopeServices(DryIoc.Container container)
        {
            // todo: implement and use with split into diff scopes: 1) init doesn't need all the stuff, even UI so will be quick
        }

        /// <summary>Shared through all session, but inner cmp to app scope initialization.</summary>
        static void RegisterSessionSingletons(DryIoc.Container container)
        {
            var tray = (TaskbarIcon)Application.Current.Resources["SysTray"];
            container.UseInstance(tray);
            container.UseInstance(new AppInfo(typeof(App).Assembly));
            container.UseInstance(TaskScheduler.FromCurrentSynchronizationContext()); // UI Tasks Scheduler
        }

        /// <summary>Adds all modules regs to container as final point.</summary>
        static void RegisterModules(DryIoc.Container container)
        {
            CommonModule.Register(container);
            DesktopCommonModule.Register(container);
            ServerModule.Register(container);
            FileSystemModule.Register(container);
            UIModule.Register(container);
        }
    }
}
#endif
