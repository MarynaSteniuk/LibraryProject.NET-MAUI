using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    private string _userEmail = string.Empty;
    public string UserEmail
    {
        get => _userEmail;
        set { _userEmail = value; OnPropertyChanged(); }
    }

    // ДОДАНО: Список для зберігання улюблених книг
    public ObservableCollection<BookModel> FavoriteBooks { get; } = new();

    public ICommand LogoutCommand { get; }
    public ICommand GoToDetailsCommand { get; }
    public ICommand LoadFavoritesCommand { get; }

    // ДОДАНО: Передали ILibraryApiService в конструктор
    public ProfileViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        Title = "Мій профіль";

        UserEmail = Preferences.Default.Get("user_email", "Гість");

        LogoutCommand = new Command(async () => await LogoutAsync());
        GoToDetailsCommand = new Command<BookModel>(async (book) => await GoToDetailsAsync(book));
        LoadFavoritesCommand = new Command(async () => await LoadFavoritesAsync());
    }

    // ДОДАНО: Метод завантаження книг із сервера
    public async Task LoadFavoritesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            if (UserEmail == "Гість" || string.IsNullOrEmpty(UserEmail)) return;

            var books = await _apiService.GetFavoriteBooksAsync(UserEmail);

            FavoriteBooks.Clear();
            foreach (var book in books)
            {
                FavoriteBooks.Add(book);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    // ДОДАНО: Відкриття сторінки деталей
    private async Task GoToDetailsAsync(BookModel? book)
    {
        if (book == null) return;
        await Shell.Current.GoToAsync($"BookDetailPage?id={book.Id}");
    }

    private async Task LogoutAsync()
    {
        Preferences.Default.Remove("user_email");
        SecureStorage.Default.Remove("auth_token");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
