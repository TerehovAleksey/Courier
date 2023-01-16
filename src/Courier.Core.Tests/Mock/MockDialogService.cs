using Courier.Core.Services;
using Xunit.Abstractions;

namespace Courier.Core.Tests.Mock;

public class MockDialogService : IDialogService
{
    private readonly ITestOutputHelper _output;
    private readonly string _promptResult;
    private readonly bool _questionResult;

    public MockDialogService(ITestOutputHelper output, string promptResult, bool questionResult)
    {
        _output = output;
        _promptResult = promptResult;
        _questionResult = questionResult;
    }

    public Task DisplayAlert(string title, string message, string cancel)
    {
        _output.WriteLine($"Alert! Title: {title}; Message: {message}");
        return Task.CompletedTask;
    }

    public Task<string> DisplayPromptAsync(string title, string message, string initialValue = "")
    {
        _output.WriteLine($"Prompt! Title: {title}; Message: {message}; Initial: {initialValue}");
        _output.WriteLine($"Prompt result: {_promptResult}");
        return Task.FromResult(_promptResult);
    }

    public Task<bool> DisplayQuestion(string title, string message, string ok, string cancel)
    {
        _output.WriteLine($"Question! Title: {title}; Message: {message}");
        _output.WriteLine($"Question result: {_questionResult}");
        return Task.FromResult(_questionResult);
    }
}
