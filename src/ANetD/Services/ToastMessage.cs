namespace ANetD.Services;

public class ToastMessage
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    public string Title { get; init; } = string.Empty;
    public string? Message { get; init; }
    public ToastVariant Variant { get; init; } = ToastVariant.Default;
    public int DurationMs { get; init; } = 4000;
    public bool IsDismissible { get; init; } = true;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}

public enum ToastVariant
{
    Default,
    Success,
    Warning,
    Danger,
    Info,
}
