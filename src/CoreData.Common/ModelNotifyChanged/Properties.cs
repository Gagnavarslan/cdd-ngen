using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CoreData.Common.ModelNotifyChanged
{
    ///// <summary>Source of property-values</summary>
    //public class PropertyValueSource
    //{
    //    //public PropertyValueSource()
    //    //{

    //    //}

    //}

    /// <summary>Property data container.
    /// <para>Reacting to Sub Property and Target Collection Changes <seealso cref="http://www.hardcodet.net/category/net/dependencies"/></para></summary>
    [DebuggerDisplay("{" + nameof(IDebugInfo.PrintValue) + "}")]
    public class Properties : IDebugInfo
    {
        public string PrintValue => _values.Select(p => $"[{p.Key}={p.Value}]").Join("\t");

        public static Properties Empty(Action<PropertyChangedEventArgs> onChanged) =>
            new Properties(onChanged, new Dictionary<string, Property>(Property.NameComparer));

        private readonly Dictionary<string, Property> _values;
        private readonly Action<PropertyChangedEventArgs> _onChanged;

        public Properties(Action<PropertyChangedEventArgs> onChanged, Dictionary<string, Property> values)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));
            _onChanged = onChanged;
        }

        /// <summary>Get property value.</summary>
        public T Get<T>(T _init = default, IEqualityComparer<T> comparer = null, [CallerMemberName]string name = null) =>
            Get(() => _init, comparer, name);
        /// <summary>Get property value.</summary>
        public T Get<T>(Func<T> _init = null, IEqualityComparer<T> comparer = null, [CallerMemberName]string name = null) =>
            GetOrAddProperty(name, _init, comparer).Value;
        /// <summary>Set property value.</summary>
        public void Set<T>(T value, [CallerMemberName]string name = null) =>
            GetOrAddProperty<T>(name, null, null).Value = value;

        /// <summary>Get existing propertydata or create a new one with the specified info.</summary>
        private Property<T> GetOrAddProperty<T>(string name,
            Func<T> init = default, IEqualityComparer<T> comparer = default)
        {
            if (!_values.TryGetValue(name, out var result))
            {
                result = new Property<T>(name, _onChanged, init, comparer);
                _values.Add(name, result);
            }
            return (Property<T>)result;
        }
    }
}
