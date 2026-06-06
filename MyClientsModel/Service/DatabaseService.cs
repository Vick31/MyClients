using MyClientsModel.Model;
using SQLite;


namespace MyClientsModel.Service
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clients.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Client>();
            await _database.CreateTableAsync<ClientAction>();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            await Init();
            return await _database!.Table<Client>().ToListAsync();
        }

        public async Task<int> SaveClientAsync(Client client)
        {
            await Init();

            if (client.Id != 0)
                return await _database!.UpdateAsync(client);

            return await _database!.InsertAsync(client);
        }

        public async Task<int> DeleteClientAsync(Client client)
        {
            await Init();
            return await _database!.DeleteAsync(client);
        }

        public async Task<List<ClientAction>> GetClientActionAsync()
        {
            await Init();
            return await _database!.Table<ClientAction>().ToListAsync();
        }

        public async Task<List<ClientAction>> GetServicesByClientAsync(int clientId)
        {
            await Init();

            return await _database!
                .Table<ClientAction>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<int> SaveClientActionAsync(ClientAction clientAction)
        {
            await Init();

            if (clientAction.Id != 0)
                return await _database!.UpdateAsync(clientAction);

            return await _database!.InsertAsync(clientAction);
        }

        public async Task<int> SaveActionAsync(ClientAction clientAction)
        {
            await Init();

            if (clientAction.Id != 0)
                return await _database!.UpdateAsync(clientAction);

            return await _database!.InsertAsync(clientAction);
        }


    }
}
