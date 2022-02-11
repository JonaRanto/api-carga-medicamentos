namespace CargaDeMedicamentosAPI.Entities
{
    public class CargaIndividualInput
    {
        public int IdSucursal { get; set; }
        public int CodigoTFC { get; set; }
    }
    public class CargaIndividualOutput
    {
        public int Precio { get; set; }
        public int Stock { get; set; }
    }
}
