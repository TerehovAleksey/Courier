namespace Courier.Controls;

public partial class InputTime : VerticalStackLayout
{
    #region Bindable properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InputTime), string.Empty, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(TimeSpan), typeof(InputTime), null, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public TimeSpan Value
    {
        get => (TimeSpan)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(InputTime), false, BindingMode.OneWay, propertyChanged: OnIsReadOnlyChanged);

    public new bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    #endregion

    public InputTime()
    {
        InitializeComponent();
    }

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputTime input)
        {
            input.InputLabel.Text = (string)newValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputTime input)
        {
            input.TimePicker.Time = (TimeSpan)newValue;
        }
    }

    private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is InputTime input)
        {
            input.TimePicker.IsEnabled = !(bool)newValue;
        }
    }

    private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
        {
            Value = ((TimePicker)sender).Time;
        }
    }

    #endregion
}