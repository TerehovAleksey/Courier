namespace Courier.Pages;

public partial class EndDayPage : PageBase
{
	public EndDayPage(EndDayPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}