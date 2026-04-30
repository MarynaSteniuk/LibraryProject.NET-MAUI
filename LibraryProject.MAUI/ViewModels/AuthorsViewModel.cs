using System.Collections.ObjectModel;
using System.Windows.Input;
using LibraryProject.MAUI.Models;
using LibraryProject.MAUI.Services;

namespace LibraryProject.MAUI.ViewModels;

public class AuthorsViewModel : BaseViewModel
{
    private readonly ILibraryApiService _apiService;

    public ObservableCollection<AuthorModel> Authors { get; } = new();

    public ICommand LoadAuthorsCommand { get; }
    public ICommand GoToAddCommand { get; }
    public ICommand DeleteCommand { get; }

    public AuthorsViewModel(ILibraryApiService apiService)
    {
        _apiService = apiService;
        Title = "Список авторів";

        LoadAuthorsCommand = new Command(async () => await LoadAuthorsAsync());
        GoToAddCommand = new Command(async () => await Shell.Current.GoToAsync("AuthorFormPage?id=0"));
        DeleteCommand = new Command<AuthorModel>(async (author) => await DeleteAuthorAsync(author));
    }

    private async Task LoadAuthorsAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            var result = await _apiService.GetAuthorsAsync();
            if (result == null) return;

            Authors.Clear();
            foreach (var author in result) Authors.Add(author);
        }
        finally { IsBusy = false; }
    }

    private async Task DeleteAuthorAsync(AuthorModel author)
    {
        if (author == null) return;
        bool confirmed = await Shell.Current.DisplayAlert("Увага", $"Видалити автора {author.Name}?", "Так", "Ні");
        if (!confirmed) return;

        IsBusy = true;
        try
        {
            await _apiService.DeleteAuthorAsync(author.Id);
            await LoadAuthorsAsync(); 
        }
        finally { IsBusy = false; }
    }
}
