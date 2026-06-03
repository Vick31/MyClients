using MyClients.Views;

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
    }
}
