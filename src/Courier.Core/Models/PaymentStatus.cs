using System.ComponentModel.DataAnnotations;

namespace Courier.Core.Models;

/// <summary>
/// Тип оплаты
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Наличные
    /// </summary>
    [Display(Name = "Наличные")]
    Money,
    /// <summary>
    /// Картой
    /// </summary>
    [Display(Name = "Карточкой")]
    Card,
    /// <summary>
    /// Оплачено на сайте
    /// </summary>
    [Display(Name = "Оплачено на сайте")]
    Site
}
