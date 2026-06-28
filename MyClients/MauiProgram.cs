using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyClients.Views;
using MyClients.Views.Calendar;
using MyClients.Views.Config;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 7 Free-Solid-900.otf", "FontAwesome");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Services
            builder.Services.AddSingleton<DatabaseService>();

            // View Model
            builder.Services.AddTransient<ClientsViewModel>();
            builder.Services.AddTransient<ClientActionViewModel>();
            builder.Services.AddTransient<ClientServicesViewModel>();
            builder.Services.AddTransient<CalendarViewModel>();

            // Forms
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientFormPage>();
            builder.Services.AddTransient<ClientActionPage>();
            builder.Services.AddTransient<ClientServicesPage>();
            builder.Services.AddTransient<CalendarPage>();
            builder.Services.AddTransient<ReminderFormPage>();
            builder.Services.AddTransient<ConfigurationPage>();

            return builder.Build();
        }
    }
}
