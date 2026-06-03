using Microsoft.Extensions.Logging;
using MyClients.Views;
using MyClientsModel.Service;
using MyClientsModel.ViewModel;

namespace MyClients
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Services
            builder.Services.AddSingleton<DatabaseService>();

            // View Model
            builder.Services.AddTransient<ClientsViewModel>();

            // Forms
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientFormPage>();

            return builder.Build();
        }
    }
}
