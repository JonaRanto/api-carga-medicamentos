using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class EcargaItemFallido
    {
        public int Id { get; set; }
        public string CodigoTfc { get; set; }
        public string CodigoInterno { get; set; }
        public string NuevoPrecio { get; set; }
        public string StockActual { get; set; }
        public string CodigoBarra { get; set; }
        public string DescripcionInterna { get; set; }
        public string DescError { get; set; }
        public int EcargaId { get; set; }

        public virtual Ecarga Ecarga { get; set; }
    }
}
