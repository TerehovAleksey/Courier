namespace Courier.Core.Models;

/// <summary>
/// Тип доставки
/// </summary>
public class DeliveryType
{
    /// <summary>
    /// ID
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Оплата за доставку
    /// </summary>
    public decimal Cost { get; set; }
}
