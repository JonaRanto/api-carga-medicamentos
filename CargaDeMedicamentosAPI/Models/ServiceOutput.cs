namespace CargaDeMedicamentosAPI.Models
{
    public class ServiceOutput
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public ServiceOutput()
        {
            Error = true;
            Message = "";
            Data = "";
        }
    }
}
