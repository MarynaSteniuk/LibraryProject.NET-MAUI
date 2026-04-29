using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class BookFormPage : ContentPage
{
    public BookFormPage(BookFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}