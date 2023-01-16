namespace Courier;

public partial class AppShell : Shell
{
	public AppShell()
	{
        Routing.RegisterRoute("fast", typeof(FastAddPage));
        Routing.RegisterRoute("delivery", typeof(DeliveryPage));
        Routing.RegisterRoute("start", typeof(StartDayPage));
        Routing.RegisterRoute("end", typeof(EndDayPage));
        Routing.RegisterRoute("day", typeof(DayStatisticsPage));
        InitializeComponent();
	}
}
