using System;

namespace CoreData.Common.Settings
{
    /// <summary>
    /// <para><seealso cref="https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/file-schema/runtime/appcontextswitchoverrides-element"/></para></summary>
    public class AppSwitchSettingsStorage : ISettingsStorage
    {
        public AppSwitchSettingsStorage()
        {
        }

        public bool Exists(string key) => AppContext.TryGetSwitch(key, out var _);

        public void Remove(string key) => throw new NotImplementedException();
        public void Write<T>(string key, T value) => throw new NotImplementedException();
        public T Read<T>(string key, T @default) => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }

    /// <summary>AppSwitch helper.</summary>
    public static class Switches
    {
        public const char Delimiter = '.';
        public const string Prefix = "Switch"; // all switches default prefix

        #region Switch namespaces(library names) - mid part of switch name
        public const string SystemIO = "System.IO";
        public const string CoreData = "CoreData";
        #endregion Switch namespaces(library names) - mid part of switch name

        #region Switch Names
        public const string SkipAppRegistration = "SkipAppRegistration";
        //public const string SpinLockReentranceTracking = "SpinLockTracking";
        #endregion Switch Names

        public static string GetFullName(this string name) => GetFullName(name, CoreData);
        internal static string GetFullName(this string name, string library) =>
            string.Join(Delimiter.ToString(), Prefix, library, name);
    }
}
