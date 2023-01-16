using Courier.Services;

namespace Courier;

//c:\Users\tereh\AppData\Local\Packages\21c63357-07c1-46b0-8b57-53ad46d0d1ff_9zz4h110yvjzm\LocalState\
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IDbParams, DbParams>();
		builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<IDialogService, DialogService>();
		builder.Services.AddTransient<ICourierService, CourierService>();
		builder.RegisterViewModels();
		builder.RegisterViews();

        return builder.Build();
	}
}

public static class MauiProgramExtensions
{
	public static void RegisterViewModels(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<SettingsPageViewModel>();
		builder.Services.AddTransient<FastAddPageViewModel>();
		builder.Services.AddTransient<DeliveryPageViewModel>();
		builder.Services.AddTransient<StatisticsPageViewModel>();
		builder.Services.AddTransient<EndDayPageViewModel>();
		builder.Services.AddTransient<DayStatisticsPageViewModel>();
		builder.Services.AddTransient<StartDayPageViewModel>();
	}

    public static void RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<FastAddPage>();
        builder.Services.AddTransient<DeliveryPage>();
        builder.Services.AddTransient<StatisticsPage>();
        builder.Services.AddTransient<EndDayPage>();
        builder.Services.AddTransient<DayStatisticsPage>();
        builder.Services.AddTransient<StartDayPage>();
    }
}
