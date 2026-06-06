using MyClientsModel.Model;
using MyClientsModel.ViewModel;

namespace MyClients.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel _viewModel;
    public ClientsPage(ClientsViewModel viewModel)
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

    private async void NewClientButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClientFormPage));
    }

    private async void Client_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Client client)
        {
            await Shell.Current.GoToAsync($"{nameof(ClientActionPage)}?ClientId={client.Id}");
        }
    }
}