using CommunityToolkit.Maui.Views;
using MyClients.Views.Modal;
using MyClientsModel.Data;

namespace MyClients.Helpers;

public static class SelectionService
{
    public static async Task<SelectionItem?> Select(string title, List<SelectionItem> items)
    {
        await Task.Yield();
        var popup = new SelectionPopup(title, items);

        var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        return result as SelectionItem;
    }
}