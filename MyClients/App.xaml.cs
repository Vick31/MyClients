using MyClients.Views;
using MyClients.Views.Calendar;
using MyClients.Views.Crafts;

namespace MyClients
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(ClientsPage), typeof(ClientsPage));
            Routing.RegisterRoute(nameof(ClientFormPage), typeof(ClientFormPage));
            Routing.RegisterRoute(nameof(ClientActionPage), typeof(ClientActionPage));
            Routing.RegisterRoute(nameof(ClientServicesPage), typeof(ClientServicesPage));
            Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));
            Routing.RegisterRoute(nameof(ReminderFormPage), typeof(ReminderFormPage));
            Routing.RegisterRoute(nameof(CraftsPage), typeof(CraftsPage));
        }
    }
}
