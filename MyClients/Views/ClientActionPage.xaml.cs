using MyClients.Helpers;
using MyClientsModel.Model;
using MyClientsModel.ViewModel;

namespace MyClients.Views;

[QueryProperty(nameof(ClientId), "ClientId")]
public partial class ClientActionPage : ContentPage
{
    private readonly ClientActionViewModel _clientActionViewModel;
    private int _clientId;

    public string ClientId
    {
        set
        {
            _clientId = int.Parse(value);
        }
    }

    public ClientActionPage(ClientActionViewModel clientActionViewModel)
    {
        InitializeComponent();

        _clientActionViewModel = clientActionViewModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (ActionTypePicker.SelectedItem == null)
        {
            await DialogService.Error("Debe seleccionar el Tipo de Servicio");
            return;
        }

        var action = new ClientAction
        {
            ClientId = _clientId,
            ActionType = ActionTypePicker.SelectedItem.ToString()!,
            ActionDate = ActionDatePicker.Date,
            Notes = NotesEditor.Text ?? "",
            Completed = false
        };

        await _clientActionViewModel.SaveClientsAsync(action);
        await DialogService.Success("Datos Guardados Correctamente");
        await Navigation.PopAsync();
    }
}