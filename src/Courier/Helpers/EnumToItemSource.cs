namespace Courier.Helpers;

public class EnumToItemSource : IMarkupExtension
{
    public Type? EnumType { get; set; }

    public object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (EnumType is not null && EnumType.IsEnum)
        {
            return Enum.GetValues(EnumType);
        }
        return null;
    }
}
