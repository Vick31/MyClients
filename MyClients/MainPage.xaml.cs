using MyClients.Views;
using MyClients.Views.Calendar;
using MyClients.Views.Crafts;
using MyClientsModel.Service;

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

        private async void BtnManualidades_Clicked(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CraftsPage));
        }

        private async void BtnBackup_Clicked(object sender, TappedEventArgs e)
        {
            try
            {
                await new DatabaseService().BackupDatabaseAsync();

                await DisplayAlert(
                    "Éxito",
                    "La copia de seguridad fue guardada.",
                    "Aceptar");
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Error",
                    ex.Message,
                    "Aceptar");
            }
        }
    }
}
