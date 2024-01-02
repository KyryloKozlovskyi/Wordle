namespace Wordle;

public partial class SettingsPage : ContentPage
{
    // Property for DarkTheme
    public bool DarkTheme
    {
        get => Application.Current.RequestedTheme == AppTheme.Dark;
        set => Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;

    }

    public SettingsPage()
    {
        InitializeComponent();
        // Bind the property to the switch
        DarkThemeSwitch.BindingContext = this;
    }
}