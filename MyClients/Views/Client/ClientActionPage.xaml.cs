using MyClients.Helpers;
using MyClientsModel.Model;
using MyClientsModel.ViewModel;

namespace MyClients.Views;

[QueryProperty(nameof(ClientId), "ClientId")]
[QueryProperty(nameof(ActionId), "ActionId")]
public partial class ClientActionPage : ContentPage
{
    private readonly ClientActionViewModel _clientActionViewModel;
    private int _clientId;
    private int _actionId;

    public string ClientId
    {
        set => _clientId = int.Parse(value);
    }
    public string ActionId
    {
        set => _actionId = int.Parse(value);
    }

    public ClientActionPage(ClientActionViewModel clientActionViewModel)
    {
        InitializeComponent();

        _clientActionViewModel = clientActionViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_actionId > 0)
        {
            var action =await _clientActionViewModel.GetActionAsync(_actionId);
         
            if (action != null)
            {
                ActionTypePicker.SelectedItem = action.ActionType;
                ActionDatePicker.Date = action.ActionDate;
                NotesEditor.Text = action.Notes;
            }
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (ActionTypePicker.SelectedItem == null)
        {
            await DialogService.Error(
                "Debe seleccionar el Tipo de Servicio");

            return;
        }

        var action = new ClientAction
        {
            Id = _actionId,
            ClientId = _clientId,
            ActionType = ActionTypePicker.SelectedItem.ToString()!,
            ActionDate = ActionDatePicker.Date,
            Notes = NotesEditor.Text ?? "",
            Completed = false
        };

        LoadingService.Show(
            _actionId == 0
                ? "Guardando información..."
                : "Actualizando información...");

        await _clientActionViewModel.SaveClientsAsync(action);

        LoadingService.Hide();

        await DialogService.Success(
            _actionId == 0
                ? "Servicio registrado correctamente."
                : "Servicio actualizado correctamente.");

        await Navigation.PopAsync();
    }
}