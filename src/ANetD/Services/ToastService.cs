namespace ANetD.Services;

public sealed class ToastService : IToastService
{
    public event Action<ToastMessage>? OnToast;

    public void Show(string title, string? message = null, ToastVariant variant = ToastVariant.Default, int durationMs = 4000)
    {
        OnToast?.Invoke(new ToastMessage
        {
            Title = title,
            Message = message,
            Variant = variant,
            DurationMs = durationMs,
        });
    }

    public void ShowSuccess(string title, string? message = null) =>
        Show(title, message, ToastVariant.Success);

    public void ShowError(string title, string? message = null) =>
        Show(title, message, ToastVariant.Danger);

    public void ShowWarning(string title, string? message = null) =>
        Show(title, message, ToastVariant.Warning);

    public void ShowInfo(string title, string? message = null) =>
        Show(title, message, ToastVariant.Info);
}
