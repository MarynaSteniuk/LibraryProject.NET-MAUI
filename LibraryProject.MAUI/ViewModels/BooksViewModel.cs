using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

public class BooksViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    public ObservableCollection<BookModel> Books { get; } = new();

    private List<BookModel> _allBooks = new();

    private string _searchQuery = string.Empty;
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnPropertyChanged();
            PerformSearch(); 
        }
    }

    private BookModel? _selectedBook;
    public BookModel? SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadBooksCommand { get; }
    public ICommand GoToDetailsCommand { get; }
    public ICommand GoToAddCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }

    public BooksViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        Title = "Каталог Книг";

        LoadBooksCommand = new Command(async () => await LoadBooksAsync());
        GoToDetailsCommand = new Command<BookModel>(async (book) => await GoToDetailsAsync(book));

        GoToAddCommand = new Command(async () =>
        {
            if (IsAdmin)
                await Shell.Current.GoToAsync("BookFormPage?id=0");
            else
                await Shell.Current.DisplayAlert("Доступ заборонено", "Тільки адміністратор може додавати книги.", "ОК");
        });

        ToggleFavoriteCommand = new Command<BookModel>(async (book) => await ToggleFavoriteAsync(book));
    }

    private async Task LoadBooksAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            var result = await _apiService.GetAllAsync();
            if (result == null || !result.Any())
            {
                await Shell.Current.DisplayAlert("Помилка", "Не вдалося завантажити дані.", "OK");
                return;
            }
            var authors = await _apiService.GetAuthorsAsync();
            string userEmail = Microsoft.Maui.Storage.Preferences.Default.Get("user_email", string.Empty);

            List<int> favoriteIds = new List<int>();
            if (!string.IsNullOrEmpty(userEmail))
            {
                var favoriteBooks = await _apiService.GetFavoriteBooksAsync(userEmail);
                favoriteIds = favoriteBooks.Select(b => b.Id).ToList();
            }

            _allBooks.Clear();
            foreach (var book in result)
            {
                var author = authors.FirstOrDefault(a => a.Id == book.AuthorId);
                book.AuthorName = author != null ? author.Name : "Невідомий автор";
                book.IsFavorite = favoriteIds.Contains(book.Id);
                _allBooks.Add(book);
            }

            PerformSearch();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void PerformSearch()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            Books.Clear();
            foreach (var book in _allBooks)
            {
                Books.Add(book);
            }
        }
        else
        {
            var lowerQuery = SearchQuery.ToLower();
            var filteredBooks = _allBooks.Where(b =>
                (b.Title != null && b.Title.ToLower().Contains(lowerQuery)) ||
                (b.AuthorName != null && b.AuthorName.ToLower().Contains(lowerQuery))
            ).ToList();

            Books.Clear();
            foreach (var book in filteredBooks)
            {
                Books.Add(book);
            }
        }
    }

    private async Task GoToDetailsAsync(BookModel? book)
    {
        if (book == null) return;
        await Shell.Current.GoToAsync($"BookDetailPage?id={book.Id}");
    }

    private async Task ToggleFavoriteAsync(BookModel? book)
    {
        if (book == null) return;
        string userEmail = Microsoft.Maui.Storage.Preferences.Default.Get("user_email", string.Empty);

        if (string.IsNullOrEmpty(userEmail))
        {
            await Shell.Current.DisplayAlert("Увага", "Будь ласка, увійдіть або зареєструйтесь, щоб додавати книги в улюблене.", "ОК");
            return;
        }
        book.IsFavorite = !book.IsFavorite;

        await _apiService.ToggleFavoriteAsync(userEmail, book.Id);
    }
}