namespace Courier.Core.UiModels;

public class WeekStatisticsUiModel
{
    public string Week { get; set; } = default!;
    public decimal Sum { get; set; }
    public decimal TotalSum { get; set; }
    public int Deliveries { get; set; }
    public List<Day> Days { get; set; } = new();
}
