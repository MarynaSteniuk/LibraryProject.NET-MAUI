using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

public class BooksViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    public ObservableCollection<BookModel> Books { get; } = new();

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
    public ICommand GoToDetailCommand { get; }
    public ICommand GoToAddCommand { get; }

    public BooksViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        Title = "Каталог Книг";

        LoadBooksCommand = new Command(async () => await LoadBooksAsync());
        GoToDetailCommand = new Command<BookModel>(async (book) => await GoToDetailAsync(book));
        GoToAddCommand = new Command(async () => await GoToAddAsync());
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

            Books.Clear();
            foreach (var book in result)
            {
                Books.Add(book);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task GoToDetailAsync(BookModel? book)
    {
        if (book == null) return;
        await Shell.Current.GoToAsync($"BookDetailPage?id={book.Id}");
    }

    private async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync("BookFormPage?id=0");
    }
}