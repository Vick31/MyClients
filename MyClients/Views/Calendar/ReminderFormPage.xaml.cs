using MyClients.Helpers;
using MyClientsModel.Model;
using MyClientsModel.Service;

namespace MyClients.Views.Calendar;

public partial class ReminderFormPage : ContentPage
{
    private readonly DatabaseService _database;

    public ReminderFormPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;

        DatePickerReminder.Date = DateTime.Today;
    }

    private async void SaveButton_Clicked(
        object sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DialogService.Error("Debe ingresar un título.");
            return;
        }

        var reminder = new Reminder
        {
            Title = TitleEntry.Text,
            Description = DescriptionEditor.Text ?? "",
            Date = DatePickerReminder.Date,
            Time = TimePickerReminder.Time,
            Completed = false
        };

        await _database.SaveReminderAsync(reminder);

        await DialogService.Success("Recordatorio guardado.");
        await Navigation.PopAsync();
    }
}