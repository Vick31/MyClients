using MyClients.Views;
using MyClients.Views.Calendar;
using MyClients.Views.Config;
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

        private  async void Calendar_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CalendarPage));
        }

        private void Task_Tapped(object sender, TappedEventArgs e)
        {

        }

        private async void Reminder_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ReminderFormPage));
        }

        private void Contact_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void Statistics_Tapped(object sender, TappedEventArgs e)
        {

        }

        private async void Configuration_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ConfigurationPage));
        }
    }
}
