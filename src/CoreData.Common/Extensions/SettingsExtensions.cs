//using System.Configuration;
//using CoreData.Common.Settings;

//namespace CoreData.Common.Extensions
//{
//    public static class SettingsExtensions
//    {
//        public static void MakePortable(ApplicationSettingsBase settings)
//        {
//            var provider = new PortableSettingsProvider(settings.GetType().Name + ".settings");
//            settings.Providers.Add(provider);
//            foreach(SettingsProperty prop in settings.Properties)
//                prop.Provider = provider;
//            settings.Reload();
//        }
//    }
//}
