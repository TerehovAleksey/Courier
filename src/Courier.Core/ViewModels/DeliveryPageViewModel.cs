namespace Courier.Core.ViewModels;

public partial class DeliveryPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;

    private DeliveryType? _selectedDeliveryType;

    public ObservableCollection<DeliveryType> DeliveryTypes { get; set; } = new();
    public DeliveryType? SelectedDeliveryType
    {
        get => _selectedDeliveryType;
        set
        {
            if (value != null)
            {
                _selectedDeliveryType = value;
                OnPropertyChanged(nameof(SelectedDeliveryType));
                Cost = _selectedDeliveryType.Cost;
            }
        }
    }

    [ObservableProperty]
    private DeliveryStatus _selectedDeliveryStatus;

    [ObservableProperty]
    private PaymentStatus _selectedPaymentStatus;

    [ObservableProperty]
    private string? _number;

    [ObservableProperty]
    private string? _address;

    [ObservableProperty]
    private TimeSpan _time = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    private decimal _price = 0;

    [ObservableProperty]
    private decimal? _cost;

    public DeliveryPageViewModel(ICourierService courierService, INavigationService navigationService)
    {
        _courierService = courierService;
        _navigationService = navigationService;

        Task.Run(async() =>
        {
            var types = await _courierService.GetDeliveryTypesAsync();
            foreach (var item in types)
            {
                DeliveryTypes.Add(item);
            }
            _selectedDeliveryType = DeliveryTypes.First();
            SelectedDeliveryType = DeliveryTypes.First();
        });
    }

    [RelayCommand]
    private async Task Save()
    {
        var delivery = new Delivery
        {
            Address = Address,
            Number = Number,
            Price = Price,
            DeliveryCost = Cost ?? 0,
            Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Time.Hours, Time.Minutes, 0),
            PaymentStatus = SelectedPaymentStatus,
            DeliveryStatus = SelectedDeliveryStatus,
            DeliveryTypeId = SelectedDeliveryType?.Id
        };

        await _courierService.AddDeliveryAsync(delivery);
        await _navigationService.GoToAsync("..");
    }
}
