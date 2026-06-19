using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MyClientsModel.Data
{
    public class Api
    {
        public static string UrlServidor { get; set; }
        public static string Token { get; set; }
        public static int TimeoutSegundos { get; set; }

        public static string HttpUrlServidor
        {
            get
            {
                var uri = UrlServidor;
                if (!uri.Contains("http"))
                    uri = "http://" + uri;
                return uri;
            }
        }

        public static void InicializarApi(string urlServidor)
        {
            urlServidor.Replace(@"\", "/");
            Api.UrlServidor = urlServidor;
        }

        public static ApiResponse ApiPOST(string urlPart, string body = "{}", string auth = null, bool comprimir = false)
        {
            var url = UrlServidor + urlPart;
            var bodyJson = "";
            if (body is string)
                bodyJson = body;
            else
                bodyJson = JsonConvert.SerializeObject(body);

            if (auth == null)
                auth = "Bearer " + Token;

            var resText = NativePOST(url, bodyJson, auth, compressHeader: comprimir);
            ApiResponse resApi = JsonConvert.DeserializeObject<ApiResponse>(resText);

            return resApi;
        }

        public static bool UsarUrlRandom = true;
        public static string RandomUrl(string uri)
        {
            if (UsarUrlRandom)
            {
                if (uri.Contains("?"))
                    uri += "&";
                else
                    uri += "?";
                uri += "rn=" + (new Random()).Next(10000, 99999);
            }
            return uri;
        }

        public static string NativePOST(string uri, string data, string auth, bool compressHeader)
        {
            if (!uri.Contains("http"))
                uri = "http://" + uri;
            uri = RandomUrl(uri);

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            try
            {
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");
                    client.DefaultRequestHeaders.Add("authorization", auth);

                    if (TimeoutSegundos > 0)
                        client.Timeout = TimeSpan.FromSeconds(TimeoutSegundos);
                    else if (TimeoutSegundos == System.Threading.Timeout.Infinite)
                        client.Timeout = Timeout.InfiniteTimeSpan;

                    var content = new StringContent(data, Encoding.UTF8, "application/json");

                    var response = client.PostAsync(new Uri(uri), content).Result;

                    response.EnsureSuccessStatusCode();

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerException;

                switch (inner)
                {
                    case UriFormatException:
                        return ErrorResponse("ERRORURI", "La URL es inválida.", inner);

                    case HttpRequestException httpEx when httpEx.Message.Contains("host name could not be parsed", StringComparison.OrdinalIgnoreCase):
                        return ErrorResponse("ERRORURI", "La URL tiene un formato inválido.", httpEx);

                    case HttpRequestException:
                        return ErrorResponse("ERRORHTTP", "Fallo en la solicitud HTTP.", inner);

                    case TaskCanceledException:
                        return ErrorResponse("ERRORTIMEOUT", "La solicitud demoró demasiado.", inner);

                    default:
                        return ErrorResponse("ERRORDESCONOCIDO", "Error inesperado interno.", inner);
                }
            }
        }
        private static string ErrorResponse(string tipo, string mensaje, Exception ex)
        {
            var resError = new ApiResponse() { Estado = tipo };
            resError.Detalle.Add($"{mensaje} → {ex.Message}");
            return JsonConvert.SerializeObject(resError);
        }

        public static async Task<string> NativeGET(string uri)
        {
            if (!uri.Contains("https"))
                uri = "https://" + uri;
            uri = RandomUrl(uri);

            using (var client = new HttpClient())
            {
                // Establecer la compresión automática y el User-Agent
                client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");

                // Configurar el timeout
                if (TimeoutSegundos > 0)
                    client.Timeout = TimeSpan.FromSeconds(TimeoutSegundos);
                else if (TimeoutSegundos == System.Threading.Timeout.Infinite)
                    client.Timeout = Timeout.InfiniteTimeSpan;

                try
                {
                    // Realizar la solicitud
                    var response = await client.GetAsync(uri);

                    // Verificar si la respuesta es exitosa
                    response.EnsureSuccessStatusCode();

                    // Leer y devolver la respuesta
                    var res = await response.Content.ReadAsStringAsync();
                    return res;
                }
                catch (HttpRequestException ex)
                {
                    var resError = new ApiResponse() { Estado = "ERROR" };
                    resError.Detalle.Add("ERRORLOCAL: " + ex.Message);
                    return JsonConvert.SerializeObject(resError);
                }
            }
        }

    }
}
