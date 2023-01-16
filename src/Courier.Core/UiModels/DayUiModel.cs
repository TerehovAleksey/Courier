namespace Courier.Core.UiModels;

public class DayUiModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string DayOfWeek { get; set; } = default!;
    public string? TimePeriod { get; set; }
    public int Count { get; set; }
    public decimal AdditionalMoney { get; set; }
    public decimal DayCost { get; set; }
    public decimal Distance { get; set; }
    public decimal Expenses { get; set; }
    public string? Note { get; set; }
}
