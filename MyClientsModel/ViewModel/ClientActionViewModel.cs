using MyClientsModel.Model;
using MyClientsModel.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyClientsModel.ViewModel
{
    public class ClientActionViewModel : BaseViewModel
    {
        private readonly DatabaseService _database;

        private ObservableCollection<ClientAction> _clientAction = new();
        public ObservableCollection<ClientAction> ClientAction
        {
            get => _clientAction;
            set
            {
                _clientAction = value;
                OnPropertyChanged();
            }
        }

        public ClientActionViewModel(DatabaseService database)
        {
            _database = database;
        }

        public async Task LoadClientsAsync()
        {
            var clientAction = await _database.GetClientActionAsync();
            ClientAction = new ObservableCollection<ClientAction>(clientAction);
        }

        public async Task SaveClientsAsync(ClientAction clientAction)
        {
            var clientActionSave = await _database.SaveClientActionAsync(clientAction);
            await LoadClientsAsync();
        }

        public async Task<ClientAction> GetActionAsync(int actionId)
        {
            var clientAction = await _database.GetClientActionIdAsync(actionId);
            return clientAction;
        }
    }
}
