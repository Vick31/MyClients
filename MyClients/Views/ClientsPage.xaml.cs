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
}