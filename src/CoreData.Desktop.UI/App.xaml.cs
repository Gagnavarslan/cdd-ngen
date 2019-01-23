using System;
using System.Windows;
using CoreData.Desktop.UI.AppScope;

namespace CoreData.Desktop.UI
{
    public partial class App : Application
    {
        public App() : this(ScopeRules.Default) { }

        public App(ScopeRules rules)
        {
            AppDomain.CurrentDomain.SetData(nameof(ScopeRules), rules);
            
            var appSession = AppSession.Create(rules);
            if (appSession != null)
            {
                Startup += (_, e) => appSession.Initialize();
                SessionEnding += async (_, e) => e.Cancel = !await appSession.Complete(e.ReasonSessionEnding);
                Exit += (_, e) => appSession.Dispose();
                return;
            }

            Environment.Exit(-1);
        }
    }
}
