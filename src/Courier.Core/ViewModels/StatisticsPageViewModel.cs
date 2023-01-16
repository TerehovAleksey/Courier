using Courier.Core.UiModels;

namespace Courier.Core.ViewModels;

public partial class StatisticsPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public StatisticsUiModel? _statisticsModel;

    public StatisticsPageViewModel(ICourierService courierService, INavigationService navigationService)
    {
        _courierService = courierService;
        _navigationService = navigationService;
    }

    public override void OnAppearing()
    {
        Task.Run(async() => 
        {
            StatisticsModel = await _courierService.GetStatisticsAsync();
        });
        
    }

    [RelayCommand]
    private async Task SelectDay(Day? day)
    {
        if (day is not null)
        {
            await _navigationService.GoToAsync($"day?date={day.Date.ToShortDateString()}");
        }
    }
}
