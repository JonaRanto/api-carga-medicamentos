using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class EprecioFarmacium
    {
        public EprecioFarmacium()
        {
            EhistorialPrecios = new HashSet<EhistorialPrecio>();
        }

        public string CodTfc { get; set; }
        public string CodFarmacia { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public string CodigoBarraFramacia { get; set; }
        public string CodigoInternoFarmacia { get; set; }
        public string DescripcionInternaFarmacia { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaActualizacionStock { get; set; }

        public virtual Efarmacium CodFarmaciaNavigation { get; set; }
        public virtual Emedicamento CodTfcNavigation { get; set; }
        public virtual ICollection<EhistorialPrecio> EhistorialPrecios { get; set; }
    }
}
