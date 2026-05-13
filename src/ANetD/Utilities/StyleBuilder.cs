using System.Text;

namespace ANetD.Utilities;

public sealed class StyleBuilder
{
    private readonly StringBuilder _sb = new();

    public StyleBuilder Add(string property, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (_sb.Length > 0) _sb.Append(';');
            _sb.Append($"{property.Trim()}:{value.Trim()}");
        }
        return this;
    }

    public StyleBuilder AddIf(string property, string? value, bool condition)
    {
        return condition ? Add(property, value) : this;
    }

    public string? Build()
    {
        var result = _sb.ToString();
        return string.IsNullOrEmpty(result) ? null : result;
    }

    public override string ToString() => Build() ?? string.Empty;
}
