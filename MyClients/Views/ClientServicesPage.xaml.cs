using MyClientsModel.Model;

namespace MyClients.Views;

[QueryProperty(nameof(ClientId), "ClientId")]
public partial class ClientServicesPage : ContentPage
{
    private readonly ClientServicesViewModel _viewModel;
    private int _clientId;

    public string ClientId
    {
        set
        {
            _clientId = int.Parse(value);
        }
    }

    public ClientServicesPage(ClientServicesViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadClientsAsync();
        var client = _viewModel.Clients.FirstOrDefault(x => x.Id == _clientId);

        if (client != null)
            _viewModel.SelectedClient = client;
    }
}