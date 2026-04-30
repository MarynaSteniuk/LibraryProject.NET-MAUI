using System.Windows.Input;
using LibraryProject.MAUI.Services;
using Microsoft.Maui.Storage;

namespace LibraryProject.MAUI.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }
    public ICommand GoToRegisterCommand { get; }

    public LoginViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        LoginCommand = new Command(async () => await LoginAsync());

        GoToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("RegisterPage"));
    }

    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Увага", "Будь ласка, введіть Email та Пароль", "ОК");
            return;
        }

        IsBusy = true;
        try
        {
            var success = await _apiService.LoginAsync(Email, Password);

            if (success)
            {
                Preferences.Default.Set("user_email", Email);
                await Shell.Current.GoToAsync("//BooksPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка", "Неправильний логін або пароль", "ОК");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}