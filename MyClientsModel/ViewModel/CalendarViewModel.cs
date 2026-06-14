using MyClientsModel.Data;
using MyClientsModel.Model;
using MyClientsModel.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyClientsModel.ViewModel;

public class CalendarViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _database;

    private DateTime _currentMonth = DateTime.Today;

    private ObservableCollection<CalendarDay> _days = [];
    public ObservableCollection<CalendarDay> Days
    {
        get => _days;
        set
        {
            _days = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Reminder> _reminders = [];
    public ObservableCollection<Reminder> Reminders
    {
        get => _reminders;
        set
        {
            _reminders = value;
            OnPropertyChanged();
        }
    }

    private CalendarDay? _selectedDay;
    public CalendarDay? SelectedDay
    {
        get => _selectedDay;
        set
        {
            _selectedDay = value;
            OnPropertyChanged();
        }
    }

    private string _selectedDateText = "Seleccione un día";
    public string SelectedDateText
    {
        get => _selectedDateText;
        set
        {
            _selectedDateText = value;
            OnPropertyChanged();
        }
    }

    public string MonthTitle => _currentMonth.ToString("MMMM yyyy");

    public CalendarViewModel(DatabaseService database)
    {
        GenerateCalendar();
        _database = database;
    }

    public void NextMonth()
    {
        _currentMonth = _currentMonth.AddMonths(1);
        GenerateCalendar();
    }

    public void PreviousMonth()
    {
        _currentMonth = _currentMonth.AddMonths(-1);
        GenerateCalendar();
    }

    private void GenerateCalendar()
    {
        var items = new List<CalendarDay>();

        var firstDay = new DateTime(
            _currentMonth.Year,
            _currentMonth.Month,
            1);

        int totalDays = DateTime.DaysInMonth(
            _currentMonth.Year,
            _currentMonth.Month);

        int offset =
            ((int)firstDay.DayOfWeek + 6) % 7;

        for (int i = 0; i < offset; i++)
        {
            items.Add(new CalendarDay());
        }

        for (int day = 1; day <= totalDays; day++)
        {
            var date = new DateTime(
                _currentMonth.Year,
                _currentMonth.Month,
                day);

            items.Add(new CalendarDay
            {
                Day = day,
                Date = date,
                IsToday = date.Date == DateTime.Today
            });
        }

        Days = new ObservableCollection<CalendarDay>(items);

        OnPropertyChanged(nameof(Days));
    }

    public async Task SelectDay(CalendarDay day)
    {
        foreach (var item in Days)
        {
            item.IsSelected = false;
        }

        day.IsSelected = true;

        SelectedDay = day;

        SelectedDateText =
            day.Date.ToString("dddd dd MMMM yyyy");

        await LoadReminders();
    }

    private async Task LoadReminders()
    {
        if (SelectedDay == null)
        {
            Reminders.Clear();
            return;
        }

        var reminders = await _database.GetRemindersByDateAsync(SelectedDay.Date);
        Reminders = new ObservableCollection<Reminder>(reminders);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}