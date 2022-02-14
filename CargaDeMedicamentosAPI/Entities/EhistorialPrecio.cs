using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class EhistorialPrecio
    {
        public int Id { get; set; }
        public string CodTfc { get; set; }
        public string CodFarmacia { get; set; }
        public string CodIsp { get; set; }
        public string CodGtin { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public string TipoActualizacion { get; set; }
        public int? PersonId { get; set; }

        public virtual EprecioFarmacium Cod { get; set; }
        public virtual Person Person { get; set; }
    }
}
