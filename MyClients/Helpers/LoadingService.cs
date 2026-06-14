using CommunityToolkit.Maui.Views;
using MyClients.Views.Modal;

namespace MyClients.Helpers;

public static class LoadingService
{
    private static LoadingPopup? _popup;

    public static void Show(string message = "⏳ Cargando información...")
    {
        if (_popup != null)
            return;

        _popup = new LoadingPopup(message);

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.CurrentPage.ShowPopupAsync(_popup);
        });
    }

    public static void Hide()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _popup?.Close();
            _popup = null;
        });
    }
}