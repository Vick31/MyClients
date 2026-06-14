using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyClientsModel.ViewModel;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    protected async Task ExecuteBusyAsync(
        Func<Task> action,
        int minimumMilliseconds = 500)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        var start = DateTime.UtcNow;

        try
        {
            await action();
        }
        finally
        {
            var elapsed =
                (int)(DateTime.UtcNow - start).TotalMilliseconds;

            var remaining = minimumMilliseconds - elapsed;

            if (remaining > 0)
                await Task.Delay(remaining);

            IsBusy = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}