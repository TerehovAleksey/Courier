using Courier.Core.Services;
using Xunit.Abstractions;

namespace Courier.Core.Tests.Mock;

public class MockNavigationService : INavigationService
{
    private readonly ITestOutputHelper output;

    public MockNavigationService(ITestOutputHelper output)
    {
        this.output = output;
    }
    public Task GoToAsync(string path)
    {
        output.WriteLine($"Navigated to: {path}");
        return Task.CompletedTask;
    }
}
