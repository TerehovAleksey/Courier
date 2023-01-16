namespace Courier.Core.ViewModels;

public partial class FastAddPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;

    public FastAddPageViewModel(ICourierService courierService, INavigationService navigationService)
    {
        _courierService = courierService;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private DateTime _date = DateTime.Now;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeSpan _startTime = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeSpan _finishTime = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int _deliveryCount;

    [ObservableProperty]
    private decimal _additionalMoney;

    [ObservableProperty]
    private decimal _distance;

    [ObservableProperty]
    private string? _note;

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        var day = new CreateDayModel
        {
            Date = Date,
            AdditionalMoney = AdditionalMoney,
            Count = DeliveryCount,
            Distance = Distance,
            Note = Note,
            TimeStart = DateTime.Now.Date.Add(StartTime),
            TimeFinish = DateTime.Now.Date.Add(FinishTime),
        };

        await _courierService.AddDayAsync(day);
        await _navigationService.GoToAsync("..");
    }

    private bool CanSave() => DeliveryCount > 1 && FinishTime > StartTime;
}
