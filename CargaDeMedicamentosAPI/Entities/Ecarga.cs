using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class Ecarga
    {
        public Ecarga()
        {
            EcargaItemFallidos = new HashSet<EcargaItemFallido>();
        }

        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaCargaInicio { get; set; }
        public DateTime FechaCargaFin { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalRegistrosCorrectos { get; set; }
        public int TotalRegistrosErroneos { get; set; }
        public string EstadoCarga { get; set; }
        public string ObservacionesCarga { get; set; }
        public string DirectorioArchivo { get; set; }
        public string CodFarmacia { get; set; }
        public string RutFarmaciaCarga { get; set; }
        public string TipoAccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? PersonId { get; set; }

        public virtual Efarmacium CodFarmaciaNavigation { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<EcargaItemFallido> EcargaItemFallidos { get; set; }
    }
}
