using MyClients.Views;

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
        }
    }
}
