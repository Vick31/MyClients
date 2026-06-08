using MyClients.Helpers;
using MyClientsModel.Model;
using MyClientsModel.Service;

namespace MyClients.Views;

[QueryProperty(nameof(ClientId), "ClientId")]
public partial class ClientFormPage : ContentPage
{
    private readonly DatabaseService _database;

    private readonly List<ColorOption> _colors;

    private ColorOption? _selectedColor;

    private int _clientId;

    private Client? _editingClient;

    public string ClientId
    {
        set
        {
            if (int.TryParse(value, out var id))
                _clientId = id;
        }
    }

    public ClientFormPage(DatabaseService database)
    {
        InitializeComponent();

        _database = database;

        _colors =
        [
            new() { Name = "Coral",         Hex = "#FFAB91" },
            new() { Name = "Rosado",        Hex = "#F48FB1" },
            new() { Name = "Lavanda",       Hex = "#CE93D8" },
            new() { Name = "Lila",          Hex = "#D1C4E9" },
            new() { Name = "Violeta Claro", Hex = "#B39DDB" },

            new() { Name = "Azul Claro",    Hex = "#90CAF9" },
            new() { Name = "Azul Cielo",    Hex = "#81D4FA" },
            new() { Name = "Turquesa",      Hex = "#80DEEA" },
            new() { Name = "Aqua",          Hex = "#80CBC4" },

            new() { Name = "Menta",         Hex = "#A5D6A7" },
            new() { Name = "Verde Claro",   Hex = "#C5E1A5" },
            new() { Name = "Pistacho",      Hex = "#DCE775" },

            new() { Name = "Vainilla",      Hex = "#FFF59D" },
            new() { Name = "Arena",         Hex = "#FFE082" },
            new() { Name = "Durazno",       Hex = "#FFCC80" },
            new() { Name = "Melón",         Hex = "#FFCCBC" },

            new() { Name = "Café Claro",    Hex = "#BCAAA4" },
            new() { Name = "Beige",         Hex = "#D7CCC8" },

            new() { Name = "Gris Perla",    Hex = "#CFD8DC" },
            new() { Name = "Gris Humo",     Hex = "#B0BEC5" }
        ];

        ColorsCollection.ItemsSource = _colors;

        _selectedColor = _colors[0];

        ColorsCollection.SelectedItem = _selectedColor;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_clientId <= 0)
            return;

        _editingClient = await _database.GetClientAsync(_clientId);

        if (_editingClient == null)
            return;

        lbTitle.Text = "Editar Cliente";

        NameEntry.Text = _editingClient.Name;
        PhoneEntry.Text = _editingClient.Phone;

        var selectedColor = _colors
            .FirstOrDefault(x => x.Hex == _editingClient.ColorHex);

        if (selectedColor != null)
        {
            _selectedColor = selectedColor;
            ColorsCollection.SelectedItem = selectedColor;

            SelectedColorPreview.Background =
                Color.FromArgb(selectedColor.Hex);

            SelectedColorLabel.Text =
                $"{selectedColor.Name} ({selectedColor.Hex})";
        }
    }

    private void ColorsCollection_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        _selectedColor =
            e.CurrentSelection.FirstOrDefault() as ColorOption;

        if (_selectedColor == null)
            return;

        SelectedColorPreview.Background =
            Color.FromArgb(_selectedColor.Hex);

        SelectedColorLabel.Text =
            $"{_selectedColor.Name} ({_selectedColor.Hex})";
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DialogService.Error(
                "El nombre del cliente es obligatorio.");

            return;
        }

        Client client;

        if (_editingClient == null)
        {
            client = new Client();
        }
        else
        {
            client = _editingClient;
        }

        client.Name = NameEntry.Text ?? "";
        client.Phone = PhoneEntry.Text ?? "";
        client.ColorHex = _selectedColor?.Hex ?? "#90CAF9";

        await _database.SaveClientAsync(client);

        if (_editingClient == null)
        {
            await DialogService.Success(
                "Cliente creado correctamente.");
        }
        else
        {
            await DialogService.Success(
                "Cliente actualizado correctamente.");
        }

        await Navigation.PopAsync();
    }
}