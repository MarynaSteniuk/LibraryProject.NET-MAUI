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

        // 10.0.2.2 - це адреса localhost для емулятора. 7181 - порт API.
        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri("https://10.0.2.2:7181/")
        });
        builder.Services.AddSingleton<LibraryApiService>();
        builder.Services.AddTransient<BooksViewModel>();
        builder.Services.AddTransient<BooksPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
