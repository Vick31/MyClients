namespace MyClients.Views;

public partial class ClientServicesPage : ContentPage
{
    private readonly ClientServicesViewModel _viewModel;

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
    }
}