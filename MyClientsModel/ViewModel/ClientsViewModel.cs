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

        public ObservableCollection<Client> Clients { get; } = new();

        public ClientsViewModel(DatabaseService database)
        {
            _database = database;
        }

        public async Task LoadClientsAsync()
        {
            Clients.Clear();

            var clients = await _database.GetClientsAsync();

            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
