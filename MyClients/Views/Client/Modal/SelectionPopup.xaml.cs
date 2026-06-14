using CommunityToolkit.Maui.Views;
using MyClientsModel.Data;
using System.Collections.ObjectModel;

namespace MyClients.Views.Modal;

public partial class SelectionPopup : Popup
{
    private readonly List<SelectionItem> _allItems;

    private readonly ObservableCollection<SelectionItem> _filteredItems = [];

    public SelectionPopup(
        string title,
        List<SelectionItem> items)
    {
        InitializeComponent();

        lblTitle.Text = title;

        _allItems = items;

        foreach (var item in _allItems)
        {
            _filteredItems.Add(item);
        }

        ItemsList.ItemsSource = _filteredItems;
    }

    private void Search_TextChanged(
        object sender,
        TextChangedEventArgs e)
    {
        var text = e.NewTextValue?.Trim() ?? string.Empty;

        var results = string.IsNullOrWhiteSpace(text)
            ? _allItems
            : _allItems.Where(x =>
                x.Title.Contains(
                    text,
                    StringComparison.OrdinalIgnoreCase))
              .ToList();

        _filteredItems.Clear();

        foreach (var item in results)
        {
            _filteredItems.Add(item);
        }
    }

    private void Item_Tapped(
        object sender,
        TappedEventArgs e)
    {
        if (e.Parameter is SelectionItem item)
        {
            Close(item);
        }
    }
}