using LibraryProject.MAUI.ViewModels;
namespace LibraryProject.MAUI.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}