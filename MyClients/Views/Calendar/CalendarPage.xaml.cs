using Microsoft.Maui.Controls.Shapes;
using MyClients.Helpers;
using MyClientsModel.ViewModel;

namespace MyClients.Views.Calendar;

public partial class CalendarPage : ContentPage
{
    private readonly CalendarViewModel _viewModel;

    public CalendarPage(CalendarViewModel calendarViewModel)
    {
        InitializeComponent();

        _viewModel = calendarViewModel;
        BindingContext = _viewModel;

        RenderCalendar();
    }

    private void PreviousMonth_Clicked(object sender, EventArgs e)
    {
        LoadingService.Show();
        _viewModel.PreviousMonth();
        RenderCalendar();
        LoadingService.Hide();
    }

    private void NextMonth_Clicked(object sender, EventArgs e)
    {
        LoadingService.Show();
        _viewModel.NextMonth();
        RenderCalendar();
        LoadingService.Hide();
    }

    private void RenderCalendar()
    {
        lblMonth.Text = _viewModel.MonthTitle;

        CalendarGrid.Children.Clear();

        int index = 0;

        foreach (var day in _viewModel.Days)
        {
            int row = index / 7;
            int column = index % 7;

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var border = new Border
            {
                BackgroundColor = (Color)Application.Current.Resources["CardBackground"],
                Stroke = new SolidColorBrush((Color)Application.Current.Resources["BorderColor"]),

                StrokeShape = new RoundRectangle
                {
                    CornerRadius = 10
                },

                Padding = 2
            };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var tap = new TapGestureRecognizer();

            tap.Tapped += async (_, _) =>
            {
                LoadingService.Show();
                await _viewModel.SelectDay(day);
                RenderCalendar();
                LoadingService.Hide();
            };

            border.GestureRecognizers.Add(tap);


            if (day.IsToday)
            {
                border.BackgroundColor = Color.FromArgb("#E3F2FD");
            }

            var layout = new VerticalStackLayout
            {
                Spacing = 2
            };

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var lblDay = new Label
            {
                Text = day.Day == 0
                    ? ""
                    : day.Day.ToString(),

                HorizontalTextAlignment =
                    TextAlignment.Center,

                FontAttributes =
                    day.IsToday
                    ? FontAttributes.Bold
                    : FontAttributes.None,

                TextColor = (Color)Application.Current.Resources["TextPrimary"]
            };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            layout.Children.Add(lblDay);

            if (day.HasEvents)
            {
                layout.Children.Add(new Label
                {
                    Text = "●",
                    FontSize = 10,
                    HorizontalTextAlignment =
                        TextAlignment.Center,
                    TextColor =
                        Color.FromArgb("#4CAF50")
                });
            }

            border.Content = layout;

            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);

            CalendarGrid.Children.Add(border);

            index++;
        }
    }

    private async void NewReminder_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ReminderFormPage));
    }
}