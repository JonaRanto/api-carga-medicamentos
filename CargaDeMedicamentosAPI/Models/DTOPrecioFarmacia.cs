using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDeMedicamentosAPI.Models
{
    public class DTOPrecioFarmacia
    {
        public string CodigoTFC { get; set; }
        public string CodigoInterno { get; set; }
        public string NuevoPrecio { get; set; }
        public string StockActual { get; set; }
        public string CodigoBarra { get; set; }
        public string DescripcionInterna { get; set; }
    }

    public class DTOEstadoImportacion
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DTOPrecioFarmacia dtoPrecioFarmacia { get; set; }
    }
}
