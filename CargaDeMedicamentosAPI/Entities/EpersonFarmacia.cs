using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class EpersonFarmacia
    {
        public int PersonId { get; set; }
        public string CodFarmacia { get; set; }

        public virtual Efarmacium CodFarmaciaNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
