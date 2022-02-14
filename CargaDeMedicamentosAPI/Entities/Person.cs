using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class Person
    {
        public Person()
        {
            Ecargas = new HashSet<Ecarga>();
            EhistorialPrecios = new HashSet<EhistorialPrecio>();
            EpersonFarmacia = new HashSet<EpersonFarmacia>();
        }

        public int Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rut { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UserId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdminLocal { get; set; }
        public bool IsUsuarioNormal { get; set; }
        public bool IsRepresentantelegal { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<Ecarga> Ecargas { get; set; }
        public virtual ICollection<EhistorialPrecio> EhistorialPrecios { get; set; }
        public virtual ICollection<EpersonFarmacia> EpersonFarmacia { get; set; }
    }
}
