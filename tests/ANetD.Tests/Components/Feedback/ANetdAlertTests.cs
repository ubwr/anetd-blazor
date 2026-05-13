using ANetD.Components.Feedback;
using ANetD.Enums;
using Bunit;
using FluentAssertions;
using Xunit;

namespace ANetD.Tests.Components.Feedback;

public class ANetdAlertTests : TestContext
{
    [Fact]
    public void Alert_RendersTitle()
    {
        var cut = RenderComponent<ANetdAlert>(p => p
            .Add(x => x.Title, "Test Alert"));

        cut.Find(".anetd-alert__title").TextContent.Should().Be("Test Alert");
    }

    [Theory]
    [InlineData(AlertVariant.Info,    "anetd-alert--info")]
    [InlineData(AlertVariant.Success, "anetd-alert--success")]
    [InlineData(AlertVariant.Warning, "anetd-alert--warning")]
    [InlineData(AlertVariant.Danger,  "anetd-alert--danger")]
    public void Alert_AppliesCorrectVariantClass(AlertVariant variant, string expectedClass)
    {
        var cut = RenderComponent<ANetdAlert>(p => p
            .Add(x => x.Variant, variant));

        cut.Find("[role='alert']").ClassList.Contains(expectedClass).Should().BeTrue();
    }

    [Fact]
    public void Alert_WhenDismissible_ShowsDismissButton()
    {
        var cut = RenderComponent<ANetdAlert>(p => p
            .Add(x => x.Dismissible, true));

        cut.Find(".anetd-alert__dismiss").Should().NotBeNull();
    }

    [Fact]
    public void Alert_WhenNotDismissible_NoDismissButton()
    {
        var cut = RenderComponent<ANetdAlert>(p => p
            .Add(x => x.Dismissible, false));

        cut.FindAll(".anetd-alert__dismiss").Should().BeEmpty();
    }
}
