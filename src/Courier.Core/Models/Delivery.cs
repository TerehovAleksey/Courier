namespace Courier.Core.Models;

/// <summary>
/// Доставка
/// </summary>
public class Delivery
{
    /// <summary>
    /// ID
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// ID рабочего дня
    /// </summary>
    public int DayId { get; set; }

    /// <summary>
    /// Номер заказа
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Время доставки
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// ID типа доставки
    /// </summary>
    public int? DeliveryTypeId { get; set; }

    /// <summary>
    /// Статус доставки
    /// </summary>
    public DeliveryStatus DeliveryStatus { get; set; }

    /// <summary>
    /// Вариант оплаты
    /// </summary>
    public PaymentStatus PaymentStatus { get; set; }

    /// <summary>
    /// Стоимость заказа
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Оплата за доставку
    /// </summary>
    public decimal DeliveryCost { get; set; }
}
