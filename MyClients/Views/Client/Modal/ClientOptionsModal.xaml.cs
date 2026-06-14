using CommunityToolkit.Maui.Views;
namespace MyClients.Views.Modal;

public partial class ClientOptionsPopup : Popup
{
    private readonly int _clientId;

    public ClientOptionsPopup(int clientId)
    {
        InitializeComponent();
        _clientId = clientId;
    }

    private async void NewService_Clicked(object sender, EventArgs e)
    {
        Close();
        await Shell.Current.GoToAsync($"{nameof(ClientActionPage)}?ClientId={_clientId}");
    }

    private async void ViewHistory_Clicked(object sender, EventArgs e)
    {
        Close();
        await Shell.Current.GoToAsync($"{nameof(ClientServicesPage)}?ClientId={_clientId}");
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private async void EditClient_Clicked(object sender, EventArgs e)
    {
        Close();

        await Shell.Current.GoToAsync( $"{nameof(ClientFormPage)}?ClientId={_clientId}");

    }
}