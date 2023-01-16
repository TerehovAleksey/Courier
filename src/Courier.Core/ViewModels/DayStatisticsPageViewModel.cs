namespace Courier.Core.ViewModels;

[QueryProperty(nameof(DateString), "date")]
public partial class DayStatisticsPageViewModel : ViewModelBase
{
    private readonly ICourierService _courierService;
    public ObservableCollection<Delivery> Deliveries { get; set; } = new();

    public string? DateString { get; set; }

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private string? _timePeriod;

    [ObservableProperty]
    private int _deliveryCount;

    [ObservableProperty]
    private decimal? _dayCost;

    [ObservableProperty]
    private decimal? _additionalMoney;

    [ObservableProperty]
    private decimal? _expenses;

    [ObservableProperty]
    private decimal? _distance;

    [ObservableProperty]
    private decimal _income = 0;



    public DayStatisticsPageViewModel(ICourierService courierService)
    {
        _courierService = courierService;
    }

    public override void OnAppearing()
    {
        Deliveries.Clear();
        Task.Run(async() => 
        {
            if (!string.IsNullOrEmpty(DateString))
            {
                var day = await _courierService.GetDayByDateAsync(DateTime.Parse(DateString));

                if (day != null)
                {
                    Income = (day.DayCost ?? 0) + (day.AdditionalMoney ?? 0) - (day.Expenses ?? 0);
                    Date = day.Date;
                    TimePeriod = $"{day.TimeStart:t} - {day.TimeFinish:t}";
                    DayCost = day.DayCost;
                    Expenses = day.Expenses;
                    Distance = day.Distance;
                    AdditionalMoney = day.AdditionalMoney;
                    DeliveryCount = day.Count ?? 0;

                    var deliveries = await _courierService.GetDeliveriesAsync(day.Id);

                    if (deliveries != null)
                    {
                        foreach (var item in deliveries)
                        {
                            Deliveries.Add(item);
                        }
                    }
                }
            }
        });
    }
}
