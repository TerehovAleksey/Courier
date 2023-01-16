namespace Courier.Pages;

public partial class DeliveryPage : PageBase
{
	public DeliveryPage(DeliveryPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}