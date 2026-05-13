using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ANetD.Services;

public interface IANetDJSInterop : IAsyncDisposable
{
    ValueTask FocusElementAsync(ElementReference element);
    ValueTask BlurElementAsync(ElementReference element);

    ValueTask TrapFocusAsync(string containerId, ElementReference element);
    ValueTask ReleaseFocusTrapAsync(string containerId);

    ValueTask AddClickOutsideListenerAsync(string listenerId, ElementReference element,
        DotNetObjectReference<object> dotnetRef, string callbackMethod);
    ValueTask RemoveClickOutsideListenerAsync(string listenerId);

    ValueTask LockBodyScrollAsync();
    ValueTask UnlockBodyScrollAsync();

    ValueTask<bool> CopyToClipboardAsync(string text);

    ValueTask ScrollToElementAsync(ElementReference element, string behavior = "smooth");

    ValueTask ObserveResizeAsync(string observerId, ElementReference element,
        DotNetObjectReference<object> dotnetRef, string callbackMethod);
    ValueTask UnobserveResizeAsync(string observerId);

    ValueTask PositionFloatingAsync(ElementReference floatingEl, ElementReference referenceEl,
        string placement, int offset = 4);

    ValueTask SetThemeAsync(string theme, ElementReference? element = null);
    ValueTask<string> GetThemeAsync(ElementReference? element = null);
    ValueTask<string> GetSystemThemePreferenceAsync();
}
