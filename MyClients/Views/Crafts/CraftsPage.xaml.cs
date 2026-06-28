using MyClients.Helpers;
using MyClientsModel.Data;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MyClients.Views.Crafts;

public partial class CraftsPage : ContentPage
{
    private string _imagenBase64 = "";
    private static readonly HttpClient _httpClient = new();

    public CraftsPage()
    {
        InitializeComponent();
    }

    private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            var foto = await MediaPicker.Default.PickPhotoAsync();
            if (foto is null)
                return;

            using var stream = await foto.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var bytes = ms.ToArray();

            var base64 = Convert.ToBase64String(bytes);

            // Tipo MIME real del archivo (image/jpeg, image/png, etc.)
            var mime = foto.ContentType ?? "image/jpeg";

            // Data URI completo
            _imagenBase64 = $"data:{mime};base64,{base64}";

            PreviewImage.Source = ImageSource.FromStream(
                () => new MemoryStream(bytes));
            PreviewImage.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private async void OnEnviarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TituloEntry.Text))
        {
            await DisplayAlert("Validación", "El título es obligatorio.", "OK");
            return;
        }

        if (string.IsNullOrEmpty(_imagenBase64))
        {
            await DisplayAlert("Validación", "Debes seleccionar una imagen.", "OK");
            return;
        }

        var request = new ManualidadRequest
        {
            Titulo = TituloEntry.Text?.Trim() ?? "",
            Categoria = CategoriaEntry.Text?.Trim() ?? "",
            Descripcion = DescripcionEditor.Text?.Trim() ?? "",
            ImagenBase64 = _imagenBase64
        };

        try
        {
            EnviarButton.IsEnabled = false;

            LoadingService.Show("Subiendo Archivos");
            var response = await _httpClient.PostAsJsonAsync("https://manualidadesvictoria.essaone.cloud/api/manualidades/Guardar", request);
        LoadingService.Hide();

            if (response.IsSuccessStatusCode)
                await DisplayAlert("Éxito", "Enviado correctamente.", "OK");
            else
                await DisplayAlert("Error", $"Falló el envío: {response.StatusCode}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error de conexión: {ex.Message}", "OK");
        }
        finally
        {
            EnviarButton.IsEnabled = true;
        }
    }
}