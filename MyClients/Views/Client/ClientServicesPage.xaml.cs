using MyClients.Helpers;
using MyClientsModel.Data;
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
        {
            _viewModel.SelectedClient = client;
            lblSelectedClient.Text = client.Name;
        }
    }

    private bool isBusy;
    private async void SelectClient_Tapped(object sender, TappedEventArgs e)
    {
        if (isBusy)
            return;

        isBusy = true;
        var selected = await SelectionService.Select(
            "👥 Seleccionar Cliente",
           _viewModel.Clients.Select(c => new SelectionItem
           {
               Id = c.Id,
               Title = c.Name,
               Emoji = "👤"
           }).ToList());

        if (selected == null)
        {
            isBusy = false;
            return;
        }

        _viewModel.SelectedClient = _viewModel.Clients.First(x => x.Id == selected.Id);
        lblSelectedClient.Text = _viewModel.SelectedClient.Name;
        isBusy = false;
    }

    private async void Action_Tapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is ClientAction clientAction)
        { 
            ClientId = clientAction.ClientId.ToString();
            await Shell.Current.GoToAsync($"{nameof(ClientActionPage)}?ClientId={clientAction.ClientId}&ActionId={clientAction.Id}");
        }
    }
}