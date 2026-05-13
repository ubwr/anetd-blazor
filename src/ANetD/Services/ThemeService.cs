namespace ANetD.Services;

public sealed class ThemeService
{
    private string _currentTheme = "light";

    public string CurrentTheme => _currentTheme;

    public event Action? OnThemeChanged;

    public void SetTheme(string theme)
    {
        if (_currentTheme == theme) return;
        _currentTheme = theme;
        OnThemeChanged?.Invoke();
    }

    public void ToggleTheme()
    {
        SetTheme(_currentTheme == "light" ? "dark" : "light");
    }
}
