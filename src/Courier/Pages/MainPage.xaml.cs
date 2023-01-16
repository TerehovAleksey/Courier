namespace Courier.Pages;

public partial class MainPage : PageBase
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

