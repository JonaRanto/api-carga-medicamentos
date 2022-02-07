using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDeMedicamentosAPI.Models
{
    public class CargaIndividualInput
    {
        public int idSucursal { get; set; }
        public int codigoTFC { get; set; }
    }
    public class CargaIndividualOutput
    {
        public int precio { get; set; }
        public int stock { get; set; }
    }
}
