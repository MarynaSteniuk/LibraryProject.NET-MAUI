using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProfileViewModel viewModel)
        {
            viewModel.LoadFavoritesCommand.Execute(null);
        }
    }
}