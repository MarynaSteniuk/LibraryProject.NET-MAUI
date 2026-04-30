using System.Windows.Input;
using LibraryProject.MAUI.Services;
using Microsoft.Maui.Storage;

namespace LibraryProject.MAUI.ViewModels;

public class RegisterViewModel : BaseViewModel
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

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(); }
    }

    public ICommand RegisterCommand { get; }
    public ICommand GoToLoginCommand { get; }

    public RegisterViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;

        RegisterCommand = new Command(async () => await RegisterAsync());
        GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task RegisterAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Помилка", "Заповніть всі поля", "ОК");
            return;
        }

        if (Password != ConfirmPassword)
        {
            await Shell.Current.DisplayAlert("Помилка", "Паролі не співпадають", "ОК");
            return;
        }

        IsBusy = true;
        try
        {
            var result = await _apiService.RegisterAsync(Email, Password);

            if (result.IsSuccess)
            {
                Preferences.Default.Set("user_email", Email);

                await Shell.Current.DisplayAlert("Успіх", "Реєстрація успішна! Тепер увійдіть.", "ОК");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка реєстрації", result.ErrorMessage, "ОК");
            }
        }
        finally { IsBusy = false; }
    }
}
