using CoreData.Common.HostEnvironment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace CoreData.Common.ModelNotifyChanged
{
    [DebuggerDisplay("{" + nameof(PrintValue) + "}")]
    public class Property : IDebugInfo //: IEquatable<Property>
    { // class PropertyDefinition https://docs.microsoft.com/en-us/dotnet/api/system.windows.markup.propertydefinition?view=netframework-4.7.2
        public virtual string PrintValue => Name;

        public static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

        private readonly PropertyChangedEventArgs _args;
        private readonly Action<PropertyChangedEventArgs> _onChanged;

        public Property(string name, Action<PropertyChangedEventArgs> onChanged)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _args = new PropertyChangedEventArgs(Name);
            _onChanged = onChanged;
        }

        public string Name { get; }
        //public static string GetName(this PropertyDefinition propertyDefinition)
        //{
        //    return $"{propertyDefinition.DeclaringType.FullName}.{propertyDefinition.Name}";
        //}

        public void InvokeChanged() => _onChanged?.Invoke(_args);

        //public bool Equals(Property other) =>
        //    string.Equals(Name, other?.Name, StringComparison.OrdinalIgnoreCase);
        //public override bool Equals(object obj) => Equals(obj as Property);
        //public override int GetHashCode() => Name.GetHashCode();
    }

    public class Property<T> : Property
    {
        public override string PrintValue => $"{Name}={_current}";
        //$"{Name}={(_initialized ? _current.ToString() : "NOT_INITIALIZED")}";

        private readonly IEqualityComparer<T> _valueComparer;
        private readonly T _initial;
        //private bool _initialized;
        private T _current;
        
        public Property(string name, Action<PropertyChangedEventArgs> onChanged,
            Func<T> init = default, IEqualityComparer<T> comparer = default)
            : base(name, onChanged)
        {
            _current = _initial = init == null ? default : init();
            //_initialized = _init == null;
            _valueComparer = comparer ?? EqualityComparer<T>.Default;
        }

        public T Value
        {
            get => _current;
            set
            {
                if (!_valueComparer.Equals(_current, value))
                {
                    //Ref
                    _current = value;
                    InvokeChanged();
                }
            }
        }
    }
}
