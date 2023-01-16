using Courier.Core.Models;

namespace Courier.Converters;

public class DeliveryPaymentStatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Delivery delivery)
        {
            switch (delivery.PaymentStatus)
            {
                case PaymentStatus.Money:
                    return Colors.Blue;
                case PaymentStatus.Card:
                    return Colors.Green;
                case PaymentStatus.Site:
                    return Colors.Red;
            };
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
