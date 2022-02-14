using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class Efarmacium
    {
        public Efarmacium()
        {
            Ecargas = new HashSet<Ecarga>();
            EpersonFarmacia = new HashSet<EpersonFarmacia>();
            EprecioFarmacia = new HashSet<EprecioFarmacium>();
        }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Numero { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Alopatica { get; set; }
        public string Homeopatica { get; set; }
        public string Movil { get; set; }
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFantasia { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }

        public virtual ICollection<Ecarga> Ecargas { get; set; }
        public virtual ICollection<EpersonFarmacia> EpersonFarmacia { get; set; }
        public virtual ICollection<EprecioFarmacium> EprecioFarmacia { get; set; }
    }
}
