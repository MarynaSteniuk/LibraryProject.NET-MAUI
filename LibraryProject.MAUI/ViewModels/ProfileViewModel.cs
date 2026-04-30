using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace LibraryProject.MAUI.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private string _userEmail = string.Empty;
    public string UserEmail
    {
        get => _userEmail;
        set { _userEmail = value; OnPropertyChanged(); }
    }

    public ICommand LogoutCommand { get; }

    public ProfileViewModel()
    {
        Title = "Мій профіль";

        UserEmail = Preferences.Default.Get("user_email", "Гість");

        LogoutCommand = new Command(async () => await LogoutAsync());
    }

    private async Task LogoutAsync()
    {
        Preferences.Default.Remove("user_email");
        SecureStorage.Default.Remove("auth_token");

        await Shell.Current.GoToAsync("//LoginPage");
    }
}
