namespace LibraryProject.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.BookDetailPage), typeof(Views.BookDetailPage));
        Routing.RegisterRoute(nameof(Views.BookFormPage), typeof(Views.BookFormPage));
        Routing.RegisterRoute("AuthorFormPage", typeof(Views.AuthorFormPage));
        Routing.RegisterRoute("RegisterPage", typeof(Views.RegisterPage));
    }
}
