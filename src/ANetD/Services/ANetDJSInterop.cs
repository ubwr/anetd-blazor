using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ANetD.Services;

public sealed class ANetDJSInterop : IANetDJSInterop
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;
    private bool _disposed;

    public ANetDJSInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    private async ValueTask<IJSObjectReference> GetModuleAsync()
    {
        _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "/_content/ANetD/js/anetd.js");
        return _module;
    }

    public async ValueTask FocusElementAsync(ElementReference element)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("focusElement", element);
    }

    public async ValueTask BlurElementAsync(ElementReference element)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("blurElement", element);
    }

    public async ValueTask TrapFocusAsync(string containerId, ElementReference element)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("trapFocus", containerId, element);
    }

    public async ValueTask ReleaseFocusTrapAsync(string containerId)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("releaseFocusTrap", containerId);
    }

    public async ValueTask AddClickOutsideListenerAsync(string listenerId, ElementReference element,
        DotNetObjectReference<object> dotnetRef, string callbackMethod)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("addClickOutsideListener", listenerId, element, dotnetRef, callbackMethod);
    }

    public async ValueTask RemoveClickOutsideListenerAsync(string listenerId)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("removeClickOutsideListener", listenerId);
    }

    public async ValueTask LockBodyScrollAsync()
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("lockBodyScroll");
    }

    public async ValueTask UnlockBodyScrollAsync()
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("unlockBodyScroll");
    }

    public async ValueTask<bool> CopyToClipboardAsync(string text)
    {
        var module = await GetModuleAsync();
        return await module.InvokeAsync<bool>("copyToClipboard", text);
    }

    public async ValueTask ScrollToElementAsync(ElementReference element, string behavior = "smooth")
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("scrollToElement", element, behavior);
    }

    public async ValueTask ObserveResizeAsync(string observerId, ElementReference element,
        DotNetObjectReference<object> dotnetRef, string callbackMethod)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("observeResize", observerId, element, dotnetRef, callbackMethod);
    }

    public async ValueTask UnobserveResizeAsync(string observerId)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("unobserveResize", observerId);
    }

    public async ValueTask PositionFloatingAsync(ElementReference floatingEl, ElementReference referenceEl,
        string placement, int offset = 4)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("positionFloating", floatingEl, referenceEl, placement, offset);
    }

    public async ValueTask SetThemeAsync(string theme, ElementReference? element = null)
    {
        var module = await GetModuleAsync();
        if (element.HasValue)
            await module.InvokeVoidAsync("setTheme", theme, element.Value);
        else
            await module.InvokeVoidAsync("setTheme", theme, null);
    }

    public async ValueTask<string> GetThemeAsync(ElementReference? element = null)
    {
        var module = await GetModuleAsync();
        if (element.HasValue)
            return await module.InvokeAsync<string>("getTheme", element.Value);
        return await module.InvokeAsync<string>("getTheme", null);
    }

    public async ValueTask<string> GetSystemThemePreferenceAsync()
    {
        var module = await GetModuleAsync();
        return await module.InvokeAsync<string>("getSystemThemePreference");
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            _disposed = true;
            if (_module is not null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
