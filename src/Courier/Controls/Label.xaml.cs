namespace Courier.Controls;

public partial class Label : Microsoft.Maui.Controls.Label
{
    #region Bindable properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(Label), string.Empty, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(Label), string.Empty, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion

    public Label()
	{
		InitializeComponent();
	}

    #region Handlers

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Label label)
        {
            label.TitleSpan.Text = (string)newValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Label label)
        {
            label.TextSpan.Text = (string)newValue;
        }
    }

    #endregion
}