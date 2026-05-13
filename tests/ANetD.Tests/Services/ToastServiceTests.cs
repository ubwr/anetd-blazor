using ANetD.Services;
using FluentAssertions;
using Xunit;

namespace ANetD.Tests.Services;

public class ToastServiceTests
{
    [Fact]
    public void Show_RaisesOnToastEvent()
    {
        var service = new ToastService();
        ToastMessage? received = null;
        service.OnToast += msg => received = msg;

        service.Show("Hello", "World", ToastVariant.Default);

        received.Should().NotBeNull();
        received!.Title.Should().Be("Hello");
        received.Message.Should().Be("World");
        received.Variant.Should().Be(ToastVariant.Default);
    }

    [Fact]
    public void ShowSuccess_SetsSuccessVariant()
    {
        var service = new ToastService();
        ToastMessage? received = null;
        service.OnToast += msg => received = msg;

        service.ShowSuccess("Done");

        received!.Variant.Should().Be(ToastVariant.Success);
    }

    [Fact]
    public void ShowError_SetsDangerVariant()
    {
        var service = new ToastService();
        ToastMessage? received = null;
        service.OnToast += msg => received = msg;

        service.ShowError("Oops");

        received!.Variant.Should().Be(ToastVariant.Danger);
    }
}
