namespace Courier.Pages;

public partial class StartDayPage : PageBase
{
	public StartDayPage(StartDayPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}