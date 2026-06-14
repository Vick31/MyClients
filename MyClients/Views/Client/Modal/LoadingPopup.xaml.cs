using CommunityToolkit.Maui.Views;

namespace MyClients.Views.Modal;

public partial class LoadingPopup : Popup
{
    public LoadingPopup(string message = "⏳ Cargando información...")
    {
        InitializeComponent();

        lblMessage.Text = message;
    }
}