using System;

namespace CoreData.Common.Settings
{
    public class FileSettingsStorage : ISettingsStorage
    {
        public FileSettingsStorage()
        {

        }

        public bool Exists(string key) => throw new NotImplementedException();
        public void Remove(string key) => throw new NotImplementedException();
        public void Write<T>(string key, T value) => throw new NotImplementedException();
        public T Read<T>(string key, T @default) => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}
