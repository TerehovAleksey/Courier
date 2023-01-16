using Courier.Core.UiModels;

namespace Courier.Core.Services;

public interface ICourierService
{
    #region Settings

    public Task<Settings> GetSettingsAsync();
    public Task SaveSettingsAsync(Settings settings);

    #endregion

    #region Delivery

    public Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync();
    public Task AddDeliveryTypeAsync(DeliveryType deliveryType);
    public Task UpdateDeliveryTypeAsync(DeliveryType deliveryType);

    /// <summary>
    /// Добавление доставки
    /// </summary>
    /// <param name="delivery"></param>
    /// <returns></returns>
    public Task AddDeliveryAsync(Delivery delivery);

    public Task<List<Delivery>?> GetDeliveriesAsync(DateTime date);
    public Task<List<Delivery>?> GetDeliveriesAsync(int dayId);

    #endregion

    #region Days

    /// <summary>
    /// Получение дня по дате
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public Task<Day?> GetDayByDateAsync(DateTime date);

    /// <summary>
    /// Получение текущего дня
    /// </summary>
    /// <returns></returns>
    public Task<Day?> GetCurrentDayAsync();

    /// <summary>
    /// Получение последнего дня
    /// </summary>
    /// <returns></returns>
    public Task<Day?> GetLastDayAsync();

    /// <summary>
    /// Добавление рабочего дня
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public Task AddDayAsync(CreateDayModel day);

    /// <summary>
    /// Начало рабочего дня
    /// </summary>
    /// <param name="startTime">Время начала</param>
    /// <param name="initialMoney">Разменные деньги</param>
    /// <returns>true - если день начат, false - если сегодня уже была запись о рабочем дне</returns>
    public Task<bool> StartDayAsync(DateTime startTime, decimal initialMoney);

    /// <summary>
    /// Завершение рабочего дня
    /// </summary>
    /// <param name="endTime">Время завершения</param>
    /// <param name="additionalMoney">Чаевые</param>
    /// <param name="distance">Дистанция за день</param>
    /// <param name="note">Примечание</param>
    /// <returns>true - если день успешно закрыт, false - если день не получен или он уже закрыт</returns>
    public Task<bool> StopDayAsync(DateTime endTime, decimal additionalMoney = 0, decimal distance = 0, string? note = null);

    /// <summary>
    /// Переоткрытие рабочего дня
    /// </summary>
    /// <returns>false - если день не найден</returns>
    public Task<bool> ReopenDayAsync();

    /// <summary>
    /// Рассчёт затрат за день
    /// </summary>
    /// <param name="distance">Дистанция за день</param>
    /// <returns></returns>
    public decimal CalculateExpenses(decimal distance, Settings settings);

    public Task UpdateDayAsync(Day day);

    #endregion

    #region Statistics

    public Task<StatisticsUiModel> GetStatisticsAsync();

    #endregion
}
