using System;

namespace CoreData.Common.Settings
{
    public interface ISettingsRegistry
    {
    }

    public interface ISettingsFactory
    {
        void ConstructUsing(ISettingsRegistry settingsRegistry);

        ISettings<T> GetOrCreate<T>();
    }

    public interface ISettings<T>
    {
        string Id { get; }

        /// <summary>Plain settings data</summary>
        // !!!: it have to be correct version 
        T Data { get; }
    }

    public interface ISettingsStorageAlt
    {
        ISettings<T> Load<T>();
        void Save<T>(T settings);
    }
}
