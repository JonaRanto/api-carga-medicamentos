using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class Emedicamento
    {
        public Emedicamento()
        {
            EprecioFarmacia = new HashSet<EprecioFarmacium>();
        }

        public string CodTfc { get; set; }
        public string CodIsp { get; set; }
        public string CodGtin { get; set; }
        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public string Presentacion { get; set; }
        public string Laboratorio { get; set; }
        public string Bioequivalente { get; set; }
        public string Estado { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Marca { get; set; }

        public virtual ICollection<EprecioFarmacium> EprecioFarmacia { get; set; }
    }
}
