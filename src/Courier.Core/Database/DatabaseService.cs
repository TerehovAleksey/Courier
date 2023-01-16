namespace Courier.Core.Database;

public class DatabaseService : IDatabaseService
{
    private readonly IDbParams _dbParams;
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(IDbParams dbParams)
    {
        _dbParams = dbParams;
        _database = new SQLiteAsyncConnection(_dbParams.DatabasePath, _dbParams.Flags);
    }

    async Task Init()
    {
        await _database.CreateTableAsync<Settings>(CreateFlags.ImplicitPK);
        await _database.CreateTableAsync<Day>(CreateFlags.ImplicitPK);
        await _database.CreateTableAsync<DeliveryType>(CreateFlags.ImplicitPK);
        await _database.CreateTableAsync<Delivery>(CreateFlags.ImplicitPK);

        var isSettingExists = await _database.Table<Settings>().CountAsync();
        if (isSettingExists == 0)
        {
            await _database.InsertAsync(new Settings
            {
                Id = 0
            });
        }

        var isDeliveryTypeExists = await _database.Table<DeliveryType>().CountAsync();
        if (isDeliveryTypeExists == 0)
        {
            await _database.InsertAsync(new DeliveryType
            {
                Id = 0,
                Name = "Драйвер",
                Cost = 0
            });
        }
    }

    #region Settings

    public async Task<Settings> GetSettingsAsync() => await _database.Table<Settings>().FirstAsync();
    public Task SaveSettingsAsync(Settings settings) => _database.UpdateAsync(settings);

    #endregion

    #region Delivery

    public async Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync() => await _database.Table<DeliveryType>().ToListAsync();
    public Task AddDeliveryTypeAsync(DeliveryType deliveryType) => _database.InsertAsync(deliveryType);
    public Task UpdateDeliveryTypeAsync(DeliveryType deliveryType) => _database.UpdateAsync(deliveryType);
    public async Task<List<Delivery>> GetDeliveriesAsync(int dayId) => await _database.Table<Delivery>()
        .Where(d => d.DayId == dayId)
        .OrderBy(d => d.Time)
        .ToListAsync();
    public Task AddDeliveryAsync(Delivery delivery) => _database.InsertAsync(delivery);
    public Task UpdateDeliveryAsync(Delivery delivery) => _database.UpdateAsync(delivery);

    #endregion

    #region Day

    public async Task<IEnumerable<Day>> GetDaysAsync() => await _database.Table<Day>().ToListAsync();

    public async Task<Day?> GetDayByDateAsync(DateTime date)
    {
        await Init();

        var days = await _database.Table<Day>()
            .ToListAsync();

        return days.Where(d => d.Date.Date == date.Date)
            .FirstOrDefault();
    }

    public Task AddDayAsync(Day day) => _database!.InsertAsync(day);

    public Task<Day?> GetLastDayAsync() =>
        _database.Table<Day>()
                 .OrderByDescending(d => d.Date)
                 .FirstOrDefaultAsync();

    public Task UpdateDayAsync(Day day) => _database.UpdateAsync(day);

    #endregion
}
