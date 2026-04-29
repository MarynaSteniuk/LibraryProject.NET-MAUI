using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

[QueryProperty(nameof(BookId), "id")]
public class BookFormViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

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

    private int _authorId;
    public int AuthorId
    {
        get => _authorId;
        set { _authorId = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public BookFormViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync("..")); 
    }

    private async Task LoadBookAsync(int id)
    {
        var authors = await _apiService.GetAuthorsAsync();
        Authors.Clear();
        foreach (var author in authors) Authors.Add(author);

            if (id == 0)
        {
            Title = "Додати книгу";
            BookTitle = string.Empty;
            Isbn = string.Empty;
            Price = 0;
            AuthorId = 0;
            return;
        }

        Title = "Редагувати книгу";
        IsBusy = true;
        try
        {
            var book = await _apiService.GetByIdAsync(id);
            if (book != null)
            {
                BookTitle = book.Title;
                Isbn = book.Isbn;
                Price = book.Price;
                AuthorId = book.AuthorId;
                SelectedAuthor = Authors.FirstOrDefault(a => a.Id == book.AuthorId);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(BookTitle) || string.IsNullOrWhiteSpace(Isbn))
        {
            await Shell.Current.DisplayAlert("Помилка", "Заповніть обов'язкові поля (Назва та ISBN)", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var book = new BookModel
            {
                Id = BookId,
                Title = BookTitle,
                Isbn = Isbn,
                Price = Price,
                AuthorId = AuthorId
            };

            if (BookId == 0)
                await _apiService.CreateAsync(book);
            else
                await _apiService.UpdateAsync(book);

            await Shell.Current.DisplayAlert("Успіх", "Збережено!", "OK");

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
    public System.Collections.ObjectModel.ObservableCollection<AuthorModel> Authors { get; } = new();

    private AuthorModel? _selectedAuthor;
    public AuthorModel? SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            _selectedAuthor = value;
            if (value != null) AuthorId = value.Id;
            OnPropertyChanged();
        }
    }
}
