namespace Courier.Core.ViewModels;

public partial class EndDayPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;

    private Settings? _settings;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeSpan? _startTime;

    private decimal _distance;
    public decimal Distance
    {
        get => _distance;
        set
        {
            if (_distance != value)
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
                if (_settings is not null)
                {
                    Expenses = _courierService.CalculateExpenses(_distance, _settings);
                }
            }
        }
    }

    [ObservableProperty]
    private decimal _additionalMoney;

    [ObservableProperty]
    private decimal _expenses;

    [ObservableProperty]
    private string? _note;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private TimeSpan _finishTime = DateTime.Now.TimeOfDay;

    public EndDayPageViewModel(ICourierService courierService, INavigationService navigationService)
    {
        _courierService = courierService;
        _navigationService = navigationService;
    }

    public override async void OnAppearing()
    {
        _settings = await _courierService.GetSettingsAsync();
        var day = await _courierService.GetCurrentDayAsync();
        if (day != null)
        {
            StartTime = day.TimeStart.TimeOfDay;
            Distance = day.Distance ?? 0;
            AdditionalMoney = day.AdditionalMoney ?? 0;
            Note = day.Note;
        }
        else
        {
            await _navigationService.GoToAsync("..");
        }
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        await _courierService.StopDayAsync(DateTime.Now.Date.Add(_finishTime), _additionalMoney, _distance, _note);
        await _navigationService.GoToAsync("..");
    }

    private bool CanSave() => _startTime is not null && FinishTime > _startTime;
}
