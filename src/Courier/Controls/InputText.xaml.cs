namespace Courier.Controls;

public partial class InputText : VerticalStackLayout
{
    #region Bindable properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InputText), string.Empty, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(InputText), string.Empty, propertyChanged: OnPlaceholderChanged);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(InputText), string.Empty, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(InputText), false, BindingMode.OneWay, propertyChanged: OnIsPasswordChanged);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(InputText), false, BindingMode.OneWay, propertyChanged: OnIsReadOnlyChanged);

    public new bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    #endregion

    public InputText()
	{
		InitializeComponent();
	}

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.InputLabel.Text = (string)newValue;
        }
    }

    private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.InputEntry.Placeholder = (string)newValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.InputEntry.Text = (string)newValue;
        }
    }

    private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.InputEntry.IsPassword = (bool)newValue;
        }
    }
    private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputText input)
        {
            input.InputEntry.IsEnabled = !(bool)newValue;
            input.InputEntry.IsReadOnly = (bool)newValue;
        }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Value = (sender as Entry)?.Text ?? string.Empty;
    }

    #endregion
}