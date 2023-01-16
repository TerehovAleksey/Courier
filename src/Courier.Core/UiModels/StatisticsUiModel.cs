namespace Courier.Core.UiModels;

public class StatisticsUiModel
{
    public int TotalCount { get; set; }
    public decimal MiddleForWeek { get; set; }
    public decimal MiddleForDelivery { get; set; }
    public List<WeekStatisticsUiModel> WeekStatistics { get; set; } = new();
}
