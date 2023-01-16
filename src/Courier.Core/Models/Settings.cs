namespace Courier.Core.Models;

/// <summary>
/// Настройки
/// </summary>
public class Settings
{
    /// <summary>
    /// ID
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Стоимость топлива за литр
    /// </summary>
    public decimal FuelCost { get; set; }

    /// <summary>
    /// Расход топлива л/100км
    /// </summary>
    public decimal FuelExpense { get; set; }

    /// <summary>
    /// Оплата за 1 час работы
    /// </summary>
    public decimal OneHourCost { get; set; }
}
