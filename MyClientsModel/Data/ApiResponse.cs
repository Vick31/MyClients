namespace MyClientsModel.Data
{
    public class ApiResponse
    {

        public string Estado { get; set; }
        public List<string> Detalle { get; set; } = new List<string>();
        public object ObjetoRes { get; set; }

        public string DetalleTexto()
        {
            var res = "";
            foreach (string texto in Detalle)
                res += " - " + texto + "\r\n";
            return res;
        }

        public ApiEstado Resultado
        {
            get
            {
                switch (Estado)
                {
                    case "OK":
                        {
                            return ApiEstado.OK;
                        }

                    case "ERROR":
                        {
                            return ApiEstado.ERROR;
                        }
                }
                return default(ApiEstado);
            }
        }

        public List<ReporteError> GetReporteError()
        {
            List<ReporteError> res = new List<ReporteError>();
            foreach (var det in Detalle)
                res.Add(new ReporteError() { TextoError = det });
            return res;
        }
    }

    public enum ApiEstado
    {
        OK,
        ERROR
    }

    public class ReporteError
    {
        public string Objeto { get; set; }
        public string TextoError { get; set; }
    }
}
