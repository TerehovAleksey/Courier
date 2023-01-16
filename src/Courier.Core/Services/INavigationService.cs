namespace Courier.Core.Services;

public interface INavigationService
{
    public Task GoToAsync(string path);
}
