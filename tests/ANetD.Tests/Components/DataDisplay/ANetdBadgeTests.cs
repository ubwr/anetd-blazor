using ANetD.Components.Feedback;
using ANetD.Enums;
using Bunit;
using FluentAssertions;
using Xunit;

namespace ANetD.Tests.Components.DataDisplay;

public class ANetdBadgeTests : TestContext
{
    [Fact]
    public void Badge_RendersChildContent()
    {
        var cut = RenderComponent<ANetdBadge>(p => p.AddChildContent("5"));

        cut.Find(".anetd-badge").TextContent.Should().Be("5");
    }

    [Theory]
    [InlineData(ComponentColor.Default,   "anetd-badge--default")]
    [InlineData(ComponentColor.Primary,   "anetd-badge--primary")]
    [InlineData(ComponentColor.Success,   "anetd-badge--success")]
    [InlineData(ComponentColor.Danger,    "anetd-badge--danger")]
    public void Badge_AppliesCorrectColorClass(ComponentColor color, string expectedClass)
    {
        var cut = RenderComponent<ANetdBadge>(p => p.Add(x => x.Color, color));

        cut.Find(".anetd-badge").ClassList.Contains(expectedClass).Should().BeTrue();
    }

    [Fact]
    public void Badge_WhenDot_HasDotClass()
    {
        var cut = RenderComponent<ANetdBadge>(p => p.Add(x => x.Dot, true));

        cut.Find(".anetd-badge").ClassList.Contains("anetd-badge--dot").Should().BeTrue();
    }
}
