using ANetD.Components.Forms;
using ANetD.Enums;
using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace ANetD.Tests.Components.Forms;

public class ANetdButtonTests : TestContext
{
    [Fact]
    public void Button_RendersWithPrimaryVariantByDefault()
    {
        var cut = RenderComponent<ANetdButton>(p => p
            .AddChildContent("Click me"));

        cut.Find("button").ClassList.Contains("anetd-button--primary").Should().BeTrue();
    }

    [Theory]
    [InlineData(ButtonVariant.Primary,   "anetd-button--primary")]
    [InlineData(ButtonVariant.Secondary, "anetd-button--secondary")]
    [InlineData(ButtonVariant.Ghost,     "anetd-button--ghost")]
    [InlineData(ButtonVariant.Danger,    "anetd-button--danger")]
    [InlineData(ButtonVariant.Link,      "anetd-button--link")]
    public void Button_AppliesCorrectVariantClass(ButtonVariant variant, string expectedClass)
    {
        var cut = RenderComponent<ANetdButton>(p => p
            .Add(x => x.Variant, variant)
            .AddChildContent("Test"));

        cut.Find("button").ClassList.Contains(expectedClass).Should().BeTrue();
    }

    [Fact]
    public void Button_WhenDisabled_HasDisabledAttribute()
    {
        var cut = RenderComponent<ANetdButton>(p => p
            .Add(x => x.Disabled, true)
            .AddChildContent("Disabled"));

        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void Button_WhenLoading_ShowsSpinner()
    {
        var cut = RenderComponent<ANetdButton>(p => p
            .Add(x => x.Loading, true)
            .AddChildContent("Loading"));

        cut.Find(".anetd-button__spinner").Should().NotBeNull();
    }

    [Fact]
    public async Task Button_WhenClicked_InvokesOnClick()
    {
        var clicked = false;
        var cut = RenderComponent<ANetdButton>(p => p
            .Add(x => x.OnClick, EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => clicked = true))
            .AddChildContent("Click"));

        await cut.Find("button").ClickAsync(new());

        clicked.Should().BeTrue();
    }

    [Fact]
    public async Task Button_WhenDisabledAndClicked_DoesNotInvokeOnClick()
    {
        var clicked = false;
        var cut = RenderComponent<ANetdButton>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.OnClick, EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => clicked = true))
            .AddChildContent("Click"));

        await cut.Find("button").ClickAsync(new());

        clicked.Should().BeFalse();
    }
}
