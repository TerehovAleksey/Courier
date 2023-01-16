using System.ComponentModel.DataAnnotations;

namespace Courier.Core.Models;

/// <summary>
/// Статус доставки
/// </summary>
public enum DeliveryStatus
{
    /// <summary>
    /// Успешная
    /// </summary>
    [Display(Name = "Доставлен")]
    Ok,
    /// <summary>
    /// Возврат заказа
    /// </summary>
    [Display(Name = "Возврат")]
    Return
}
