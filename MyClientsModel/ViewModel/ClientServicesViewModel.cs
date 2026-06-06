using MyClientsModel.Model;
using MyClientsModel.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ClientServicesViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _database;

    public ObservableCollection<Client> _clients = new();
    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set
        {
            _clients = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ClientAction> _clientAction = new();
    public ObservableCollection<ClientAction> ClientAction
    {
        get => _clientAction;
        set
        {
            _clientAction = value;
            OnPropertyChanged();
        }
    }

    private Client? _selectedClient;
    public Client? SelectedClient
    {
        get => _selectedClient;
        set
        {
            _selectedClient = value;
            OnPropertyChanged();

            if (value != null)
                _ = LoadServicesAsync(value.Id);
        }
    }

    public ClientServicesViewModel(DatabaseService database)
    {
        _database = database;
    }

    public async Task LoadClientsAsync()
    {
        Clients.Clear();

        var clients = await _database.GetClientsAsync();
        Clients = new ObservableCollection<Client>(clients);
    }

    private async Task LoadServicesAsync(int clientId)
    {
        ClientAction.Clear();

        var services = await _database.GetServicesByClientAsync(clientId);
        ClientAction = new ObservableCollection<ClientAction>(services);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}