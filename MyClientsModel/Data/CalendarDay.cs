namespace MyClientsModel.Data
{
    public class CalendarDay
    {
        public int Day { get; set; }
        public DateTime Date { get; set; }
        public bool HasEvents { get; set; }
        public bool IsToday { get; set; }
        public bool IsSelected { get; set; }
    }
}
