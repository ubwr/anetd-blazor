namespace ANetD.Services;

public interface IToastService
{
    event Action<ToastMessage>? OnToast;

    void Show(string title, string? message = null, ToastVariant variant = ToastVariant.Default, int durationMs = 4000);
    void ShowSuccess(string title, string? message = null);
    void ShowError(string title, string? message = null);
    void ShowWarning(string title, string? message = null);
    void ShowInfo(string title, string? message = null);
}
