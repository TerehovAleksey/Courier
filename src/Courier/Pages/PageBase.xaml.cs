namespace Courier.Pages;

public partial class PageBase : ContentPage
{
    public IList<IView> PageContent => ContentGrid.Children;

    public PageBase()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        if(BindingContext is ViewModelBase viewModel)
        {
            viewModel.OnAppearing();
        }
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        if (BindingContext is ViewModelBase viewModel)
        {
            viewModel.OnDisappearing();
        }
        base.OnDisappearing();
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is ViewModelBase viewModel)
        {
            viewModel.OnBackButtonPressed();
        }
        return base.OnBackButtonPressed();
    }
}