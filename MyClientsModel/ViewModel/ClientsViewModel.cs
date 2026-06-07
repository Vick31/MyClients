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

        private List<Client> _allClients = new();

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

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;
                OnPropertyChanged();

                FilterClients();
            }
        }

        public ClientsViewModel(DatabaseService database)
        {
            _database = database;
        }

        public async Task LoadClientsAsync()
        {
            var clients = await _database.GetClientsAsync();

            _allClients = clients;

            Clients = new ObservableCollection<Client>(_allClients);
        }

        private void FilterClients()
        {
            IEnumerable<Client> filtered = _allClients;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string search = SearchText.Trim().ToLower();

                filtered = filtered.Where(c =>
                    (c.Name?.ToLower().Contains(search) ?? false) ||
                    (c.Phone?.ToLower().Contains(search) ?? false));
            }

            Clients = new ObservableCollection<Client>(filtered);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}