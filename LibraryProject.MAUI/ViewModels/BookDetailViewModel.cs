using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

[QueryProperty(nameof(BookId), "id")]
public class BookDetailViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;
    private BookModel? _currentBook;

    private int _bookId;
    public int BookId
    {
        get => _bookId;
        set
        {
            _bookId = value;
            _ = LoadBookAsync(value); 
        }
    }

    private string _bookTitle = string.Empty;
    public string BookTitle
    {
        get => _bookTitle;
        set { _bookTitle = value; OnPropertyChanged(); }
    }

    private string _isbn = string.Empty;
    public string Isbn
    {
        get => _isbn;
        set { _isbn = value; OnPropertyChanged(); }
    }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set { _price = value; OnPropertyChanged(); }
    }

    public ICommand DeleteCommand { get; }
    public ICommand GoToEditCommand { get; }

    public BookDetailViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        Title = "Деталі книги";

        DeleteCommand = new Command(async () => await DeleteBookAsync());
        GoToEditCommand = new Command(async () => await GoToEditAsync());
    }
    private string _authorName = string.Empty;
    public string AuthorName
    {
        get => _authorName;
        set { _authorName = value; OnPropertyChanged(); }
    }
    private async Task LoadBookAsync(int id)
    {
        IsBusy = true;
        try
        {
            _currentBook = await _apiService.GetByIdAsync(id);
            if (_currentBook != null)
            {
                BookTitle = _currentBook.Title;
                Isbn = _currentBook.Isbn;
                Price = _currentBook.Price;

                var authors = await _apiService.GetAuthorsAsync();
                var author = authors.FirstOrDefault(a => a.Id == _currentBook.AuthorId);

                AuthorName = author != null ? author.Name : "Невідомий автор";
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteBookAsync()
    {
        if (_currentBook == null) return;
        bool confirmed = await Shell.Current.DisplayAlert("Увага", "Видалити запис?", "Так", "Ні");
        if (!confirmed) return;
        IsBusy = true;
        try
        {
            await _apiService.DeleteAsync(_currentBook.Id);
            await Shell.Current.GoToAsync(".."); 
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task GoToEditAsync()
    {
        if (_currentBook == null) return;
        await Shell.Current.GoToAsync($"BookFormPage?id={_currentBook.Id}");
    }
}
