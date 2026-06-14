using MyClients.Views;
using MyClients.Views.Calendar;

namespace MyClients
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ClientsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ClientsPage));
        }

        private async void ServiceButton_Clicked(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ClientServicesPage));
        }

        private async void CalendarButton_Clicked(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CalendarPage));
        }
    }
}
