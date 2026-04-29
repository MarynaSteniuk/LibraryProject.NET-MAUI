using LibraryProject.MAUI.ViewModels;

namespace LibraryProject.MAUI.Views;

public partial class BookDetailPage : ContentPage
{
    public BookDetailPage(BookDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}