using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}