namespace CoreData.Desktop.UI.Converters
{
    public sealed class BooleanValueInverter : BooleanConverter<bool>
    {
        public BooleanValueInverter() : base(false, true) { }
    }
}
