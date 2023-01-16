namespace Courier.Services;

public class DialogService : IDialogService
{
    public Task DisplayAlert(string title, string message, string cancel) =>
        Shell.Current.DisplayAlert(title, message, cancel);

    public Task<bool> DisplayQuestion(string title, string message, string ok, string cancel) =>
        Shell.Current.DisplayAlert(title, message, ok, cancel);

    public Task<string> DisplayPromptAsync(string title, string message, string initialValue = "") =>
        Shell.Current.DisplayPromptAsync(title, message, initialValue: initialValue);
}
