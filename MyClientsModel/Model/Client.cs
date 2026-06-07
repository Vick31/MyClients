using SQLite;

namespace MyClientsModel.Model
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;
    }
}
