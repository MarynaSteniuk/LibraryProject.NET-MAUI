using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class BooksPage : ContentPage
{
    private readonly BooksViewModel _viewModel;

    public BooksPage(BooksViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; 
        _viewModel = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadBooksCommand.Execute(null);
    }
}