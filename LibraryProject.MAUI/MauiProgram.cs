using Microsoft.Extensions.Logging;
using LibraryProject.MAUI.Services;
using LibraryProject.MAUI.ViewModels;
using LibraryProject.MAUI.Views;

namespace LibraryProject.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string apiUrl = DeviceInfo.Platform == DevicePlatform.Android
             ? "https://10.0.2.2:7181/"
             : "https://localhost:7181/";

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        });
        builder.Services.AddSingleton<ILibraryApiService, LibraryApiService>();
        builder.Services.AddTransient<BooksViewModel>();
        builder.Services.AddTransient<BookDetailViewModel>();
        builder.Services.AddTransient<BookFormViewModel>();
        builder.Services.AddTransient<BooksPage>();
        builder.Services.AddTransient<BookDetailPage>();
        builder.Services.AddTransient<BookFormPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
