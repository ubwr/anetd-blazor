using System.Text;

namespace ANetD.Utilities;

public sealed class CssBuilder
{
    private readonly StringBuilder _sb = new();

    public CssBuilder Add(string? cssClass)
    {
        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            if (_sb.Length > 0) _sb.Append(' ');
            _sb.Append(cssClass.Trim());
        }
        return this;
    }

    public CssBuilder AddIf(string? cssClass, bool condition)
    {
        return condition ? Add(cssClass) : this;
    }

    public CssBuilder AddIf(string? cssClass, Func<bool> condition)
    {
        return condition() ? Add(cssClass) : this;
    }

    public string Build() => _sb.ToString();

    public override string ToString() => Build();

    public static string From(params (string? cls, bool condition)[] pairs)
    {
        var builder = new CssBuilder();
        foreach (var (cls, condition) in pairs)
            builder.AddIf(cls, condition);
        return builder.Build();
    }
}
