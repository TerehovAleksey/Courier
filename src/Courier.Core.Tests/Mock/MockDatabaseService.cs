using Courier.Core.Database;
using Courier.Core.Models;

namespace Courier.Core.Tests.Mock;

public class MockDatabaseService : IDatabaseService
{
    public List<Day> Days { get; set; } = new List<Day>();

    public Task<Day?> GetDayByDateAsync(DateTime date)
    {
        var result = Days.FirstOrDefault(x=>x.Date == date.Date);
        return Task.FromResult(result);
    }

    public Task AddDayAsync(Day day)
    {
        Days.Add(day);
        return Task.CompletedTask;
    }

    public Task<Settings> GetSettingsAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveSettingsAsync(Settings settings)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddDeliveryTypeAsync(DeliveryType deliveryType)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDeliveryTypeAsync(DeliveryType deliveryType)
    {
        throw new NotImplementedException();
    }

    public Task<List<Delivery>> GetDeliveriesAsync(int dayId)
    {
        throw new NotImplementedException();
    }

    public Task AddDeliveryAsync(Delivery delivery)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDeliveryAsync(Delivery delivery)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Day>> GetDaysAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Day?> GetLastDayAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateDayAsync(Day day)
    {
        throw new NotImplementedException();
    }
}
