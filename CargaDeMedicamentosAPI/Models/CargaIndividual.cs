namespace CargaDeMedicamentosAPI.Models
{
    public class CargaIndividualInput
    {
        public string IdSucursal { get; set; }
        public string CodigoTFC { get; set; }
        public int IdPersona { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
    public class CargaIndividualOutput
    {
        public int Precio { get; set; }
        public int Stock { get; set; }
    }
}
