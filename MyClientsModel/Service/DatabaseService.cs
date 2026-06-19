using CommunityToolkit.Maui.Storage;
using MyClientsModel.Model;
using SQLite;

namespace MyClientsModel.Service
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private string GetDatabasePath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "clients.db3");
        }

        private async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = GetDatabasePath();

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Client>();
            await _database.CreateTableAsync<ClientAction>();
            await _database.CreateTableAsync<Reminder>();
        }

        public async Task BackupDatabaseAsync()
        {
            await Init();

            string sourcePath = GetDatabasePath();

            if (!File.Exists(sourcePath))
                throw new Exception("La base de datos no existe.");

            // Cerrar SQLite temporalmente
            if (_database != null)
            {
                await _database.CloseAsync();
                _database = null;
            }

            string fileName =
                $"MyClients_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db3";

            byte[] bytes = await File.ReadAllBytesAsync(sourcePath);

            using var stream = new MemoryStream(bytes);

            var result = await FileSaver.Default.SaveAsync(
                fileName,
                stream,
                CancellationToken.None);

            if (!result.IsSuccessful)
                throw new Exception(result.Exception?.Message ?? "No se pudo guardar la copia.");
        }


        public async Task<List<Client>> GetClientsAsync()
        {
            await Init();
            return await _database!.Table<Client>().ToListAsync();
        }

        public async Task<Client?> GetClientAsync(int id)
        {
            await Init();

            return await _database!
                .Table<Client>()
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<ClientAction> GetClientActionIdAsync(int id)
        {
            await Init();

            return await _database!
                .Table<ClientAction>()
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<List<Reminder>> GetRemindersByDateAsync(DateTime date)
        {
            await Init();

            var start = date.Date;
            var end = start.AddDays(1);

            return await _database!
                .Table<Reminder>()
                .Where(x => x.Date >= start && x.Date < end)
                .ToListAsync();
        }

        public async Task<int> SaveReminderAsync(Reminder reminder)
        {
            await Init();

            if (reminder.Id != 0)
                return await _database!.UpdateAsync(reminder);

            return await _database!.InsertAsync(reminder);
        }


    }
}
