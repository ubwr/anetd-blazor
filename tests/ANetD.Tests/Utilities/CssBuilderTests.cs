using ANetD.Utilities;
using FluentAssertions;
using Xunit;

namespace ANetD.Tests.Utilities;

public class CssBuilderTests
{
    [Fact]
    public void Add_SingleClass_ReturnsClass()
    {
        var result = new CssBuilder().Add("foo").Build();
        result.Should().Be("foo");
    }

    [Fact]
    public void Add_MultipleClasses_JoinsWithSpace()
    {
        var result = new CssBuilder().Add("foo").Add("bar").Build();
        result.Should().Be("foo bar");
    }

    [Fact]
    public void AddIf_True_IncludesClass()
    {
        var result = new CssBuilder().AddIf("foo", true).Build();
        result.Should().Be("foo");
    }

    [Fact]
    public void AddIf_False_ExcludesClass()
    {
        var result = new CssBuilder().AddIf("foo", false).Build();
        result.Should().BeEmpty();
    }

    [Fact]
    public void Add_NullOrEmpty_IsIgnored()
    {
        var result = new CssBuilder().Add(null).Add("").Add("  ").Add("bar").Build();
        result.Should().Be("bar");
    }
}
