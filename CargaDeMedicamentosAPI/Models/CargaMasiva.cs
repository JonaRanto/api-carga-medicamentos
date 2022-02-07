using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDeMedicamentosAPI.Models
{
    public class CargaMasiva
    {
        public string codigoTFC { get; set; }
        public string codigoInterno { get; set; }
        public string precio { get; set; }
        public string stock { get; set; }
        public string codigoBarra { get; set; }
        public string descripcionInterna { get; set; }
    }
}
