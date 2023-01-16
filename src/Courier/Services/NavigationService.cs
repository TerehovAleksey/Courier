namespace Courier.Services;

public class NavigationService : INavigationService
{
    public Task GoToAsync(string path) => Shell.Current.GoToAsync(path);
}
