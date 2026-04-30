using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class AuthorsPage : ContentPage
{
    private readonly AuthorsViewModel _viewModel;
    public AuthorsPage(AuthorsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadAuthorsCommand.Execute(null);
    }
}