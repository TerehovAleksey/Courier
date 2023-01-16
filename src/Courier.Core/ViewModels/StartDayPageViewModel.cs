namespace Courier.Core.ViewModels;

public partial class StartDayPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private TimeSpan _startTime = DateTime.Now.TimeOfDay;

    [ObservableProperty]
    private decimal _initialMoney = 0;

    public StartDayPageViewModel(ICourierService courierService, INavigationService navigationService, IDialogService dialogService)
    {
        _courierService = courierService;
        _navigationService = navigationService;
        _dialogService = dialogService;
    }

    [RelayCommand(CanExecute = nameof(CanStartDay))]
    private async Task StartDay()
    {
        var result = await _courierService.StartDayAsync(DateTime.Now.Date.Add(_startTime), _initialMoney);
        if (result)
        {
            await _navigationService.GoToAsync("..");
        }
        else
        {
            result = await _dialogService.DisplayQuestion("Ошибка", "Рабочий день сегодня уже закрыт. Переоткрыть для продолжения?", "Да", "Нет");
            if (result)
            {
                await _courierService.ReopenDayAsync();
            }
            await _navigationService.GoToAsync("..");
        }
    }

    private bool CanStartDay()
    {
        return _initialMoney >= 0;
    }
}
