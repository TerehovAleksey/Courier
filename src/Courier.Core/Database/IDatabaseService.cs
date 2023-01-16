namespace Courier.Core.Database;

public interface IDatabaseService
{
    #region Settings

    public Task<Settings> GetSettingsAsync();
    public Task SaveSettingsAsync(Settings settings);

    #endregion

    #region Delivery

    public Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync();
    public Task AddDeliveryTypeAsync(DeliveryType deliveryType);
    public Task UpdateDeliveryTypeAsync(DeliveryType deliveryType);
    public Task<List<Delivery>> GetDeliveriesAsync(int dayId);
    public Task AddDeliveryAsync(Delivery delivery);
    public Task UpdateDeliveryAsync(Delivery delivery);

    #endregion

    #region Day

    public Task<IEnumerable<Day>> GetDaysAsync();
    public Task<Day?> GetDayByDateAsync(DateTime date);
    public Task<Day?> GetLastDayAsync();
    public Task AddDayAsync(Day day);
    public Task UpdateDayAsync(Day day);

    #endregion
}
