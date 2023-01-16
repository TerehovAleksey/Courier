using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Courier.Converters;

public class EnumDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            var fi = value.GetType().GetField(value.ToString() ?? string.Empty);
            if (fi is not null)
            {
                var attr = fi.GetCustomAttribute<DisplayAttribute>(false);
                if(attr is not null)
                {
                    return attr?.Name ?? string.Empty;
                }
            }
            return value?.ToString() ?? string.Empty;
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
