namespace Courier.Core.ViewModels;

public partial class MainPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private bool _isDayOpen;

    [ObservableProperty]
    private TimeSpan _startTime = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    private int _count;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private decimal _cost;

    public MainPageViewModel(ICourierService courierService, INavigationService navigationService)
    {
        _courierService = courierService;
        _navigationService = navigationService;
    }

    public override void OnAppearing()
    {
        Task.Run(async () =>
        {
            var day = await _courierService.GetCurrentDayAsync();
            if (day != null && day.TimeFinish is null)
            {
                IsDayOpen = true;
                StartTime = day.TimeStart.TimeOfDay;
                Count = day.Count ?? 0;
                Cost = day.DayCost ?? 0;
                Price = day.CashMoney ?? 0;
            }
            else
            {
                IsDayOpen = false;
                StartTime = DateTime.Now.TimeOfDay;
                Count = 0;
                Cost = 0;
                Price = 0;
            }
        });
    }

    [RelayCommand]
    private Task StartDay() => _navigationService.GoToAsync("start");

    [RelayCommand]
    private Task FinishDay() => _navigationService.GoToAsync("end");

    [RelayCommand]
    private async Task Add()
    {
        if (IsDayOpen)
        {
           await _navigationService.GoToAsync("delivery");
        }
        else
        {
            await _navigationService.GoToAsync("fast");
        }
    }
}
