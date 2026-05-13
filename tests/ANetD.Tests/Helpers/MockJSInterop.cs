using ANetD.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;

namespace ANetD.Tests.Helpers;

public static class MockJSInteropFactory
{
    public static Mock<IANetDJSInterop> Create()
    {
        var mock = new Mock<IANetDJSInterop>();
        mock.Setup(x => x.FocusElementAsync(It.IsAny<ElementReference>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.BlurElementAsync(It.IsAny<ElementReference>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.TrapFocusAsync(It.IsAny<string>(), It.IsAny<ElementReference>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.ReleaseFocusTrapAsync(It.IsAny<string>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.LockBodyScrollAsync()).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.UnlockBodyScrollAsync()).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.CopyToClipboardAsync(It.IsAny<string>())).ReturnsAsync(true);
        mock.Setup(x => x.ScrollToElementAsync(It.IsAny<ElementReference>(), It.IsAny<string>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.PositionFloatingAsync(It.IsAny<ElementReference>(), It.IsAny<ElementReference>(), It.IsAny<string>(), It.IsAny<int>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.SetThemeAsync(It.IsAny<string>(), It.IsAny<ElementReference?>())).Returns(ValueTask.CompletedTask);
        mock.Setup(x => x.GetThemeAsync(It.IsAny<ElementReference?>())).ReturnsAsync("light");
        mock.Setup(x => x.GetSystemThemePreferenceAsync()).ReturnsAsync("light");
        mock.Setup(x => x.DisposeAsync()).Returns(ValueTask.CompletedTask);
        return mock;
    }
}
