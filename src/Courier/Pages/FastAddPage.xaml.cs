namespace Courier.Pages;

public partial class FastAddPage : PageBase
{
	public FastAddPage(FastAddPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}