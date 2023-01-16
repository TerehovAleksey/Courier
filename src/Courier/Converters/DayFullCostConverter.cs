using Courier.Core.Models;

namespace Courier.Converters;

public class DayFullCostConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Day day && day is not null)
        {
            return (day.DayCost ?? 0) + (day.AdditionalMoney ?? 0);
        }

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
