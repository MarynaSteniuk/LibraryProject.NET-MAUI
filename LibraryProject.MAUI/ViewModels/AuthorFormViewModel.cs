using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

[QueryProperty(nameof(AuthorId), "id")]
public class AuthorFormViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    private int _authorId;
    public int AuthorId
    {
        get => _authorId;
        set { _authorId = value; _ = LoadAuthorAsync(value); }
    }

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    // Нове поле: Національність
    private string _nationality = string.Empty;
    public string Nationality
    {
        get => _nationality;
        set { _nationality = value; OnPropertyChanged(); }
    }

    // Нове поле: Дата народження
    private DateTime _birthDate = DateTime.Now;
    public DateTime BirthDate
    {
        get => _birthDate;
        set { _birthDate = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AuthorFormViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task LoadAuthorAsync(int id)
    {
        if (id == 0)
        {
            Title = "Додати автора";
            Name = string.Empty;
            Nationality = string.Empty;
            BirthDate = new DateTime(1990, 1, 1); // Початкова дата
            return;
        }

        Title = "Редагувати автора";
        IsBusy = true;
        try
        {
            var author = await _apiService.GetAuthorByIdAsync(id);
            if (author != null)
            {
                Name = author.Name;
                Nationality = author.Nationality ?? string.Empty;
                BirthDate = author.BirthDate;
            }
        }
        finally { IsBusy = false; }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть ім'я автора", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var author = new AuthorModel
            {
                Id = AuthorId,
                Name = Name,
                Nationality = Nationality,
                BirthDate = BirthDate
            };

            if (AuthorId == 0) await _apiService.CreateAuthorAsync(author);
            else await _apiService.UpdateAuthorAsync(author);

            await Shell.Current.DisplayAlert("Успіх", "Автора збережено!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        finally { IsBusy = false; }
    }
}
