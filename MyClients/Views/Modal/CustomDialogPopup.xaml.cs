using CommunityToolkit.Maui.Views;

namespace MyClients.Views.Modal;

public partial class CustomDialogPopup : Popup
{
    public CustomDialogPopup(
        string icon,
        string title,
        string message,
        string buttonText = "👍 Entendido")
    {
        InitializeComponent();

        lblIcon.Text = icon;
        lblTitle.Text = title;
        lblMessage.Text = message;
        btnAccept.Text = buttonText;
    }

    private void Accept_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}