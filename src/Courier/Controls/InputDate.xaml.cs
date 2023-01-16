namespace Courier.Controls;

public partial class InputDate : VerticalStackLayout
{
    #region Bindable properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InputDate), string.Empty, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(DateTime), typeof(InputDate), null, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public DateTime Value
    {
        get => (DateTime)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(InputDate), false, BindingMode.OneWay, propertyChanged: OnIsReadOnlyChanged);

    public new bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    #endregion

    public InputDate()
	{
		InitializeComponent();
	}

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputDate input)
        {
            input.InputLabel.Text = (string)newValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputDate input)
        {
            input.DatePicker.Date = (DateTime)newValue;
        }
    }

    private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputDate input)
        {
            input.DatePicker.IsEnabled = !(bool)newValue;
        }
    }

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        Value = ((DatePicker)sender).Date;
    }

    #endregion

}