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
    }
}
