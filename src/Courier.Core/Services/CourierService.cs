using Courier.Core.UiModels;
using System.Globalization;

namespace Courier.Core.Services;

public class CourierService : ICourierService
{
    private readonly IDatabaseService _databaseService;

    public CourierService(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    #region Settings

    public Task<Settings> GetSettingsAsync() => _databaseService.GetSettingsAsync();

    public Task SaveSettingsAsync(Settings settings) => _databaseService.SaveSettingsAsync(settings);

    #endregion

    #region Delivery

    public Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync() => _databaseService.GetDeliveryTypesAsync();

    public Task AddDeliveryTypeAsync(DeliveryType deliveryType) => _databaseService.AddDeliveryTypeAsync(deliveryType);

    public Task UpdateDeliveryTypeAsync(DeliveryType deliveryType) => _databaseService.UpdateDeliveryTypeAsync(deliveryType);

    public async Task AddDeliveryAsync(Delivery delivery)
    {
        var day = await GetCurrentDayAsync();

        if (day == null)
        {
            return;
        }

        delivery.DayId = day.Id;

        await _databaseService.AddDeliveryAsync(delivery);

        if (day.Count is null)
        {
            day.Count = 1;
        }
        else
        {
            day.Count++;
        }

        if (day.DayCost is null)
        {
            day.DayCost = delivery.DeliveryCost;
        }
        else
        {
            day.DayCost += delivery.DeliveryCost;
        }

        var paymentStatus = delivery.PaymentStatus;
        if (paymentStatus == PaymentStatus.Money)
        {
            if (day.CashMoney is null)
            {
                day.CashMoney = delivery.Price;
            }
            else
            {
                day.CashMoney += delivery.Price;
            }
        }
        else if (paymentStatus == PaymentStatus.Card)
        {
            if (day.NonCashMoney is null)
            {
                day.NonCashMoney = delivery.Price;
            }
            else
            {
                day.NonCashMoney += delivery.Price;
            }
        }

        await _databaseService.UpdateDayAsync(day);
    }

    public async Task<List<Delivery>?> GetDeliveriesAsync(DateTime date)
    {
        var day = await GetDayByDateAsync(date);
        if (day == null)
        {
            return null;
        }

        return await GetDeliveriesAsync(day.Id);
    }

    public Task<List<Delivery>?> GetDeliveriesAsync(int dayId) => _databaseService.GetDeliveriesAsync(dayId);

    #endregion

    #region Days

    public Task<Day?> GetDayByDateAsync(DateTime date) => _databaseService.GetDayByDateAsync(date);

    public Task<Day?> GetCurrentDayAsync() => _databaseService.GetDayByDateAsync(DateTime.Now);

    public Task<Day?> GetLastDayAsync() => _databaseService.GetLastDayAsync();

    public async Task AddDayAsync(CreateDayModel day)
    {
        var deliveryTypes = await _databaseService.GetDeliveryTypesAsync();
        var deliveryCost = deliveryTypes.FirstOrDefault()?.Cost ?? 0;
        var settings = await _databaseService.GetSettingsAsync();

        var newDay = new Day
        {
            Date = day.Date,
            TimeStart = day.TimeStart,
            TimeFinish = day.TimeFinish,
            AdditionalMoney = day.AdditionalMoney,
            CashMoney = 0,
            NonCashMoney = 0,
            Note = day.Note,
            Count = day.Count,
            Distance = day.Distance,
            Expenses = CalculateExpenses(day.Distance, settings),
            DayCost = day.Count * deliveryCost
        };

        await _databaseService.AddDayAsync(newDay);
    }

    public async Task<bool> StartDayAsync(DateTime startTime, decimal initialMoney)
    {
        var isExists = await _databaseService.GetDayByDateAsync(startTime);

        if (isExists != null)
        {
            return false;
        }

        var day = new Day
        {
            Date = startTime.Date,
            TimeStart = startTime,
            CashMoney = initialMoney
        };

        await _databaseService.AddDayAsync(day);

        return true;
    }

    public async Task<bool> StopDayAsync(DateTime endTime, decimal additionalMoney = 0, decimal distance = 0, string? note = null)
    {
        var day = await _databaseService.GetLastDayAsync();
        var settings = await _databaseService.GetSettingsAsync();
        if (day == null || day.TimeFinish is not null)
        {
            return false;
        }
        day.AdditionalMoney = additionalMoney;
        day.Distance = distance;
        day.Note = note;
        day.TimeFinish = endTime;
        day.Expenses = CalculateExpenses(distance, settings);

        await _databaseService.UpdateDayAsync(day);
        return true;
    }

    public async Task<bool> ReopenDayAsync()
    {
        var day = await _databaseService.GetLastDayAsync();
        if (day != null)
        {
            day.TimeFinish = null;
            await _databaseService.UpdateDayAsync(day);
            return true;
        }
        return false;
    }

    public Task UpdateDayAsync(Day day) => _databaseService.UpdateDayAsync(day);

    public decimal CalculateExpenses(decimal distance, Settings settings) => distance * settings.FuelExpense / 100 * settings.FuelCost;

    #endregion

    #region Statistics

    public async Task<StatisticsUiModel> GetStatisticsAsync()
    {
        Func<DateTime, int> weekProjector =
            d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

        var days = await _databaseService.GetDaysAsync();

        var groupingDays = from day in days.OrderByDescending(d => d.Date)
                           group day by weekProjector(day.Date);

        List<WeekStatisticsUiModel> statistics = new();

        foreach (var item in groupingDays.OrderByDescending(g => g.Key))
        {
            var st = new WeekStatisticsUiModel
            {
                Days = item.ToList(),
                Week = item.Key.ToString(),
                Sum = item.Sum(x => x.DayCost) ?? 0,
                TotalSum = item.Select(x => (x.DayCost ?? 0) + (x.AdditionalMoney ?? 0) - (x.Expenses ?? 0)).Sum(x => x),
                Deliveries = item.Sum(x => x.Count ?? 0)
            };
            statistics.Add(st);
        }

        var model = new StatisticsUiModel
        {
            WeekStatistics = statistics,
            TotalCount = statistics.Sum(s => s.Deliveries),
            MiddleForWeek = statistics.Sum(s => s.Deliveries) / statistics.Count,
            MiddleForDelivery = CourierService.CalculateMiddleForDelivery(statistics)
        };

        return model;
    }

    private static decimal CalculateMiddleForDelivery(List<WeekStatisticsUiModel> weeks)
    {
        int totalCount = 0;
        decimal totalSum = 0;
        foreach (var week in weeks)
        {
            // для рассчёта среднего дохода от доставки исключаем дни без затрат
            var days = week.Days.Where(d => d.Expenses is not null && d.Expenses > 0);
            totalCount += days.Sum(d => d.Count ?? 0);
            totalSum += days.Sum(d => d.DayCost ?? 0 + d.AdditionalMoney ?? 0 - d.Expenses ?? 0);
        }

        if (totalCount == 0)
        {
            return 0;
        }
        return totalSum / totalCount;
    }

    #endregion
}
