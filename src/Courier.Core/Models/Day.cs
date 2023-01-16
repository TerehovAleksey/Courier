namespace Courier.Core.Models;

/// <summary>
/// Рабочий день
/// </summary>
public class Day
{
    /// <summary>
    /// ID
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Время начала работы
    /// </summary>
    public DateTime TimeStart { get; set; }

    /// <summary>
    /// Время окончания работы
    /// </summary>
    public DateTime? TimeFinish { get; set; }

    /// <summary>
    /// Количество заказов
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Чаевые за день
    /// </summary>
    public decimal? AdditionalMoney { get; set; }

    /// <summary>
    /// Заработано за доставки за день
    /// </summary>
    public decimal? DayCost { get; set; }

    /// <summary>
    /// Наличные за день
    /// </summary>
    public decimal? CashMoney { get; set; }

    /// <summary>
    /// Безнал за день
    /// </summary>
    public decimal? NonCashMoney { get; set; }

    /// <summary>
    /// Километров за день
    /// </summary>
    public decimal? Distance { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Затраты за день
    /// </summary>
    public decimal? Expenses { get; set; }
}

public class CreateDayModel
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Время начала работы
    /// </summary>
    public DateTime TimeStart { get; set; }

    /// <summary>
    /// Время окончания работы
    /// </summary>
    public DateTime TimeFinish { get; set; }

    /// <summary>
    /// Количество заказов
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Чаевые за день
    /// </summary>
    public decimal AdditionalMoney { get; set; }

    /// <summary>
    /// Километров за день
    /// </summary>
    public decimal Distance { get; set; }

    /// <summary>
    /// Примечание
    /// </summary>
    public string? Note { get; set; }
}
