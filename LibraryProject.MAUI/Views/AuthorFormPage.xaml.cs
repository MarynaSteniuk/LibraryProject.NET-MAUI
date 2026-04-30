using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class AuthorFormPage : ContentPage
{
    public AuthorFormPage(AuthorFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}