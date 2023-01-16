namespace Courier.Core.Services;

public interface IDialogService
{
    public Task DisplayAlert(string title, string message, string cancel);
    public Task<bool> DisplayQuestion(string title, string message, string ok, string cancel);
    public Task<string> DisplayPromptAsync(string title, string message, string initialValue = "");
}
