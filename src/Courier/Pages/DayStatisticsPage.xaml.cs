namespace Courier.Pages;

public partial class DayStatisticsPage : PageBase
{
	public DayStatisticsPage(DayStatisticsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}