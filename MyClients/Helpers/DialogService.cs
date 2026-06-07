using CommunityToolkit.Maui.Views;
using MyClients.Views.Modal;

namespace MyClients.Helpers;

public static class DialogService
{
    public static async Task Info(string message)
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(
            new CustomDialogPopup(
                "ℹ️",
                "Información",
                message));
    }

    public static async Task Warning(string message)
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(
            new CustomDialogPopup(
                "⚠️",
                "Advertencia",
                message));
    }

    public static async Task Error(string message)
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(
            new CustomDialogPopup(
                "❌",
                "Error",
                message));
    }

    public static async Task Success(string message)
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(
            new CustomDialogPopup(
                "🎉",
                "Éxito",
                message,
                "🚀 Continuar"));
    }
}