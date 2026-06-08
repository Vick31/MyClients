using CommunityToolkit.Maui.Views;

namespace MyClients.Views.Modal;

public partial class CustomDialogPopup : Popup
{
    public CustomDialogPopup(
        string icon,
        string title,
        string message,
        DialogType type,
        string buttonText = "👍 Entendido")
    {
        InitializeComponent();

        lblIcon.Text = icon;
        lblTitle.Text = title;
        lblMessage.Text = message;
        btnAccept.Text = buttonText;

        ApplyStyle(type);
    }

    private void ApplyStyle(DialogType type)
    {
        switch (type)
        {
            case DialogType.Success:
                btnAccept.BackgroundColor = Color.FromArgb("#22C55E");
                break;

            case DialogType.Warning:
                btnAccept.BackgroundColor = Color.FromArgb("#F59E0B");
                break;

            case DialogType.Error:
                btnAccept.BackgroundColor = Color.FromArgb("#EF4444");
                break;

            default:
                btnAccept.BackgroundColor = Color.FromArgb("#06B6D4");
                break;
        }

        btnAccept.TextColor = Colors.White;
    }

    private void Accept_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
