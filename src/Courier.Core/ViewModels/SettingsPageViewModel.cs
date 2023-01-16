namespace Courier.Core.ViewModels;

public partial class SettingsPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    private readonly IDialogService _dialogService;

    private Settings? settings;

    [ObservableProperty]
    private decimal _fuelCost;

    [ObservableProperty]
    private decimal _fuelExpense;

    [ObservableProperty]
    private decimal _oneHourCost;

    [ObservableProperty]
    private DeliveryType? _selectedDeliveryType;

    public ObservableCollection<DeliveryType> DeliveryTypes { get; set; } = new();

    public SettingsPageViewModel(ICourierService courierService, IDialogService dialogService)
    {
        _courierService = courierService;
        _dialogService = dialogService;
    }

    public override void OnAppearing()
    {
        DeliveryTypes.Clear();
        Task.Run( async() => 
        {
            settings = await _courierService.GetSettingsAsync();
            FuelCost = settings.FuelCost;
            FuelExpense = settings.FuelExpense;
            OneHourCost = settings.OneHourCost;

            var deliveryTypes = await _courierService.GetDeliveryTypesAsync();
            
            foreach (var item in deliveryTypes)
            {
                DeliveryTypes.Add(item);
            }
        });
    }

    public override void OnDisappearing()
    {
        if (settings != null)
        {
            Task.Run(async () =>
            {
                settings.FuelCost = FuelCost;
                settings.FuelExpense = FuelExpense;
                settings.OneHourCost = OneHourCost;
                await _courierService.SaveSettingsAsync(settings);
            });
        }
    }

    [RelayCommand]
    private async Task AddDeliveryType()
    {
        string name = await _dialogService.DisplayPromptAsync("Новый тип доставки", "Введите название");

        if (string.IsNullOrEmpty(name))
        {
            await _dialogService.DisplayAlert("Ошибка", "Название не может быть пустым", "Закрыть");
            return;
        }

        string value = await _dialogService.DisplayPromptAsync("Новый тип доставки", "Введите стоимость", "0");
        _ = decimal.TryParse(value, out decimal cost);
        var deliveryType = new DeliveryType
        {
            Name = name,
            Cost = cost
        };

        await _courierService.AddDeliveryTypeAsync(deliveryType);

        DeliveryTypes.Add(deliveryType);
    }

    [RelayCommand]
    private async Task EditDeliveryType()
    {
        if (SelectedDeliveryType is null)
        {
            return;
        }

        string name = await _dialogService.DisplayPromptAsync("Редактирование типа доставки", "Введите название", SelectedDeliveryType.Name);
        if (string.IsNullOrEmpty(name))
        {
            await _dialogService.DisplayAlert("Ошибка", "Название не может быть пустым", "Закрыть");
            return;
        }

        string value = await _dialogService.DisplayPromptAsync("Редактирование типа доставки", "Введите стоимость", SelectedDeliveryType.Cost.ToString());
        _ = decimal.TryParse(value.Replace(".",","), out decimal cost);

        var deliveryType = new DeliveryType
        {
            Id = SelectedDeliveryType.Id,
            Name = name,
            Cost = cost
        };

        await _courierService.UpdateDeliveryTypeAsync(deliveryType);

        SelectedDeliveryType = null;

        var deliveryTypes = await _courierService.GetDeliveryTypesAsync();
        DeliveryTypes.Clear();
        foreach (var item in deliveryTypes)
        {
            DeliveryTypes.Add(item);
        }
    }
}
