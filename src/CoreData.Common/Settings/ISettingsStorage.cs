using System;

namespace CoreData.Common.Settings
{
    /// <summary>Represents settings storage.</summary>
    public interface ISettingsStorage : IDisposable
    {
        /// <summary>Returns true or false to indicate if a setting with the specified key already exists.</summary>
        bool Exists(string key);

        /// <summary>Removes the specified setting from the storage.</summary>
        void Remove(string key);

        /// <summary>Writes the value of type T to the setting with the specified key. Note that the object T must be serializable otherwise an exception will occur.</summary>
        void Write<T>(string key, T value);

        /// <summary>Returns an object of type T for the setting with the specified key. If the setting doesn't exist, the "default" value is returned.</summary>
        T Read<T>(string key, T @default);
    }
    //public interface ISettings<T>
    //{
    //    //AppContext.TryGetSwitch();

    //    Uri RelativePath { get; }

    //    T Default { get; }

    //    T Load();

    //    void Save(T value);
    //}
}
