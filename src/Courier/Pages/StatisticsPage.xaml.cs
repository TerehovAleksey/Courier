namespace Courier.Pages;

public partial class StatisticsPage : PageBase
{
	public StatisticsPage(StatisticsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}