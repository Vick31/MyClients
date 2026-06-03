using MyClientsModel.Model;
using MyClientsModel.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyClientsModel.ViewModel
{   
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _database;

        private ObservableCollection<Client> _clients = new();
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public ClientsViewModel(DatabaseService database)
        {
            _database = database;
        }

        public async Task LoadClientsAsync()
        {
            var clients = await _database.GetClientsAsync();

            Clients = new ObservableCollection<Client>(clients);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
