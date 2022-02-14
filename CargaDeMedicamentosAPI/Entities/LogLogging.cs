using System;
using System.Collections.Generic;

#nullable disable

namespace CargaDeMedicamentosAPI.Entities
{
    public partial class LogLogging
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Type { get; set; }
        public string IpAddress { get; set; }
        public DateTime OpenSession { get; set; }
        public DateTime? CloseSession { get; set; }
    }
}
