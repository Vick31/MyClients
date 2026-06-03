using MyClientsModel.Model;
using MyClientsModel.Service;

namespace MyClients.Views;

public partial class ClientFormPage : ContentPage
{
    private readonly DatabaseService _database;

    public ClientFormPage(DatabaseService database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var client = new Client
        {
            Name = NameEntry.Text ?? "",
            Phone = PhoneEntry.Text ?? ""
        };

        await _database.SaveClientAsync(client);

        await DisplayAlert("Success", "Client saved", "OK");

        await Navigation.PopAsync();
    }
}