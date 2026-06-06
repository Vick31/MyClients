using SQLite;

namespace MyClientsModel.Model
{
    public class ClientAction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}
